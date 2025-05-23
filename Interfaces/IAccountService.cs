﻿using DocBookAPI.DTOs;
using DocBookAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace DocBookAPI.Interfaces
{
    public interface IAccountService
    {
        Task<IdentityResult> RegisterAsync(RegisterDTO register);
        Task<AuthResponseDTO> AuthenticateUserAsync(LoginDto model);
        Task<ProfileDTO> UpdateUserProfileAsync(string id, ProfileDTO user);
        Task<bool> DeleteUserAsync(string userId);
        Task<IEnumerable<ApplicationUser>> GetAllUsersAsync();
        Task<ApplicationUser> GetUserByIdAsync(string id);
        Task<string> GetUserRoleAsync(string email);
        Task<ApplicationUser> GetUserAsync(string email);
        Task<string> GetRoleFromToken(string token);
        Task<IdentityResult> ResetPasswordAsync(ApplicationUser user, string newPassword);
        //Task<List<User>> GetAllPatients();
        //Task<List<User>> GetAllDoctors();
    }

}
