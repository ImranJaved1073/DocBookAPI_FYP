using DocBookAPI.DTOs;
using DocBookAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace DocBookAPI.Interfaces
{
    public interface IAccountService
    {
        Task<IdentityResult> RegisterAsync(RegisterDTO register);
        Task<AuthResponseDTO> AuthenticateUserAsync(LoginDto model);
        Task<bool> UpdateUserProfileAsync(ApplicationUser updatedUser);
        Task<bool> DeleteUserAsync(string userId);
        Task<IEnumerable<ApplicationUser>> GetAllUsersAsync();
        Task<ApplicationUser> GetUserByIdAsync(string id);
        Task<ApplicationUser> GetUserAsync(string email);
        //Task<List<User>> GetAllPatients();
        //Task<List<User>> GetAllDoctors();
    }

}
