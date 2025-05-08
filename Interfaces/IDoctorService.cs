using DocBookAPI.DTOs;
using DocBookAPI.Models;

namespace DocBookAPI.Interfaces
{
    public interface IDoctorService
    {
        Task<Doctor> GetDoctorByIdAsync(int id);
        Task<Doctor> GetDoctorByEmailAsync(string email);
        Task<Doctor> GetDoctorByUserNameAsync(string userName);
        Task<IEnumerable<Doctor>> GetAllDoctorsAsync();
        Task<DoctorDTO> AddDoctorAsync(DoctorDTO doctor);
        Task<DoctorDTO> UpdateDoctorAsync(int id, DoctorDTO doctorDto);
        Task<bool> DeleteDoctorAsync(int id);
    }
}
