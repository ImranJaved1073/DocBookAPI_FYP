
using DocBookAPI.Data;
using DocBookAPI.DTOs;
using DocBookAPI.Interfaces;
using DocBookAPI.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace DocBookAPI.Services
{
    public class AccountService : IAccountService
    {
        private readonly ApplicationDbContext _context;



        public AccountService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterDTO register)
        {
            if (await _context.Users.AnyAsync(u => u.Email == register.Email))
            {
                return new AuthResponseDto { Message = "Email already exists", IsSuccess = false };
            }

            var user = new User
            {
                Email = register.Email,
                UserName = register.UserName,
                Role = register.Role,
                PasswordHash = HashPassword(register.Password),
                Gender = register.Gender,
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new AuthResponseDto { Message = "User registered successfully", IsSuccess = true };
        }

        //public async Task<AuthResponseDto> AuthenticateUserAsync(LoginDto model)
        //{
        //    var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);

        //    if (user == null || string.IsNullOrEmpty(user.PasswordHash) || !VerifyPassword(model.Password!, user.PasswordHash))
        //    {
        //        return new AuthResponseDto { Message = "Invalid email or password", IsSuccess = false };
        //    }

        //    return new AuthResponseDto { Message = "Login successful", IsSuccess = true };
        //}

        public async Task<AuthResponseDto> AuthenticateUserAsync(LoginDto model)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email && u.PasswordHash == HashPassword(model.Password!));

            if (user == null)
            {
                return new AuthResponseDto { Message = "Invalid credentials", IsSuccess = false };
            }
            else
            {
                return new AuthResponseDto { Message = "Login successful", IsSuccess = true };
            }

        }


        //get all users
        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetUserAsync(int id)
        {
            return (await _context.Users.FindAsync(id))!;
        }

        public async Task<User> UpdateUserProfileAsync(string userId, User updatedUser)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user != null)
            {
                user.UserName = updatedUser.UserName;
                user.Email = updatedUser.Email;
                user.Phone = updatedUser.Phone;
                user.Address = updatedUser.Address;
                user.PasswordHash = updatedUser.PasswordHash;
                user.Address = updatedUser.Address;
                user.ProfilePicture = updatedUser.ProfilePicture;
                user.DateOfBirth = updatedUser.DateOfBirth;
                user.Gender = updatedUser.Gender;
                user.Role = updatedUser.Role;

                await _context.SaveChangesAsync();
            }
            
            return user!;
        }

        public async Task<User> GetUserAsync(string email)
        {
            return (await _context.Users.FirstOrDefaultAsync(u => u.Email == email))!;
        }

        public async Task<bool> DeleteUserAsync(string userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                return false;
            }
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<User>> GetAllPatients()
        {
            return await _context.Users.Where(u => u.Role == "Patient").ToListAsync();
        }

        public async Task<List<User>> GetAllDoctors()
        {
            return await _context.Users.Where(u => u.Role == "Doctor").ToListAsync();
        }


        private string HashPassword(string password)
        {
            byte[] salt = Encoding.UTF8.GetBytes("CustomSaltForHashing");
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 32));
        }

        private bool VerifyPassword(string password, string storedHash)
        {
            return HashPassword(password) == storedHash;
        }
    }
}
