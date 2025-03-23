using DocBookAPI.Models;

namespace DocBookAPI.Interfaces
{
    public interface IPatientService
    {
        Task<Patient> GetPatientByIdAsync(int id);
        Task<Patient> GetPatientByEmailAsync(string email);
        Task<Patient> GetPatientByUserNameAsync(string userName);
        Task<IEnumerable<Patient>> GetAllPatientsAsync();
        Task<Patient> AddPatientAsync(Patient patient);
        Task<Patient> UpdatePatientAsync(Patient patient);
        Task<bool> DeletePatientAsync(int id);
    }
}
