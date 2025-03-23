using DocBookAPI.DTOs;
using DocBookAPI.Models;

namespace DocBookAPI.Interfaces
{
    public interface IAccountService
    {
        Task<AuthResponseDto> RegisterAsync(RegisterDTO register);
        Task<AuthResponseDto> AuthenticateUserAsync(LoginDto model);
        Task<User> UpdateUserProfileAsync(string userId, User updatedUser);
        Task<bool> DeleteUserAsync(string userId);
        Task<List<User>> GetAllUsersAsync();
        Task<User> GetUserAsync(int id);
        Task<User> GetUserAsync(string email);
        Task<List<User>> GetAllPatients();
        Task<List<User>> GetAllDoctors();
    }

}
