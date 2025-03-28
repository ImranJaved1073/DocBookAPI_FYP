using DocBookAPI.DTOs;
using DocBookAPI.Models;

namespace DocBookAPI.Interfaces
{
    public interface IPatientService
    {
        Task<Patient> GetPatientByIdAsync(int id);
        Task<Patient> GetPatientByEmailAsync(string email);
        Task<Patient> GetPatientByUserNameAsync(string userName);
        Task<IEnumerable<Patient>> GetAllPatientsAsync();
        Task<PatientDTO> AddPatientAsync(PatientDTO patient);
        Task<PatientDTO> UpdatePatientAsync(PatientDTO patient);
        Task<bool> DeletePatientAsync(int id);
    }
}
