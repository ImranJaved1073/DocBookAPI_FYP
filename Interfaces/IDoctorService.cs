using DocBookAPI.Models;

namespace DocBookAPI.Interfaces
{
    public interface IDoctorService
    {
        Task<Doctor> GetDoctorByIdAsync(int id);
        Task<Doctor> GetDoctorByEmailAsync(string email);
        Task<Doctor> GetDoctorByUserNameAsync(string userName);
        Task<IEnumerable<Doctor>> GetAllDoctorsAsync();
        Task<Doctor> AddDoctorAsync(Doctor doctor);
        Task<Doctor> UpdateDoctorAsync(Doctor doctor);
        Task<bool> DeleteDoctorAsync(int id);
    }
}
