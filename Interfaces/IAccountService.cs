using DocBookAPI.DTOs;
using DocBookAPI.Models;

namespace DocBookAPI.Interfaces
{
    public interface IAccountService
    {
        Task<List<User>> GetAllUsersAsync();
        Task<User> GetUserAsync(int id);
        Task<AuthResponseDto> RegisterAsync(RegisterDTO register);
        Task<User> UpdateUserProfileAsync(string userId, User updatedUser);
        Task<bool> DeleteUserAsync(string userId);
        Task<AuthResponseDto> AuthenticateUserAsync(LoginDto model);
    }

}
