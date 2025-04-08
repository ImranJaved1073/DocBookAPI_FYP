
using DocBookAPI.Data;
using DocBookAPI.DTOs;
using DocBookAPI.Interfaces;
using DocBookAPI.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DocBookAPI.Services
{
    public class AccountService : IAccountService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<AccountService> _logger;
        private readonly string _key;
        private readonly string _issuer;
        private readonly string _audience;

        public AccountService(IConfiguration configuration, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager, ILogger<AccountService> logger)
        {
            _configuration = configuration;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _logger = logger;
            _key = _configuration["Jwt:Key"]!;
            _issuer = _configuration["Jwt:Issuer"]!;
            _audience = _configuration["Jwt:Audience"]!;
        }

        public async Task<IdentityResult> RegisterAsync(RegisterDTO registerDTO)
        {
            var existingUser = await _userManager.FindByEmailAsync(registerDTO.Email);
            if (existingUser != null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "Email already exists" });
            }
            var user = new ApplicationUser
            {
                UserName = registerDTO.UserName,
                Email = registerDTO.Email,
                Gender = registerDTO.Gender,
                EmailConfirmed = true,
            };
            var result = await _userManager.CreateAsync(user, registerDTO.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, registerDTO.Role);
            }
            return result;
        }

        public async Task<AuthResponseDTO> AuthenticateUserAsync(LoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email!);
            if (user == null)
            {
                return new AuthResponseDTO { Message = "Invalid email or password", IsSuccess = false };
            }

            // 🔹 Ensure email is confirmed
            if (!user.EmailConfirmed)
            {
                return new AuthResponseDTO { Message = "Please confirm your email before logging in.", IsSuccess = false };
            }

            // 🔹 Ensure user is not locked out
            if (await _userManager.IsLockedOutAsync(user))
            {
                return new AuthResponseDTO { Message = "Your account is locked. Try again later.", IsSuccess = false };
            }

            // 🔹 Manually check password before attempting login
            if (!await _userManager.CheckPasswordAsync(user, model.Password!))
            {
                await _userManager.AccessFailedAsync(user); // Increase failed attempt count
                return new AuthResponseDTO { Message = "Invalid email or password", IsSuccess = false };
            }

            // ✅ Reset failed attempt count on successful login
            await _userManager.ResetAccessFailedCountAsync(user);

            var token = await GenerateJwtToken(user);
            return new AuthResponseDTO { Token = token, IsSuccess = true, Message = "Login successful" };
        }

        // generate a function to get role of a user from the jwt token
        public async Task<string> GetRoleFromToken(string token)
        {
            return await Task.Run(() =>
            {
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(token);
                var roleClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
                return roleClaim?.Value ?? string.Empty;
            });
        }


        private async Task<string> GenerateJwtToken(ApplicationUser user)
        {
            var key = Encoding.ASCII.GetBytes(_key);
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Email, user.Email!),
                    new Claim(ClaimTypes.Role, (await _userManager.GetRolesAsync(user)).FirstOrDefault()!)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _issuer,
                Audience = _audience
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<ApplicationUser> GetUserAsync(string email)
        {
            return (await _userManager.FindByEmailAsync(email))!;
        }

        public async Task<ApplicationUser> GetUserByIdAsync(string id)
        {
            return (await _userManager.FindByIdAsync(id))!;
        }

        public async Task<ApplicationUser> GetUserByUserNameAsync(string userName)
        {
            return (await _userManager.FindByNameAsync(userName))!  ;
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllUsersAsync()
        {
            return await _userManager.Users.ToListAsync();
        }

        public async Task<string> GetUserRoleAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return string.Empty;
            }

            var roles = await _userManager.GetRolesAsync(user);
            return roles.FirstOrDefault() ?? string.Empty;
        }

        public async Task<bool> DeleteUserAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateUserProfileAsync(ApplicationUser user)
        {
            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;
        }

        


    }
}
