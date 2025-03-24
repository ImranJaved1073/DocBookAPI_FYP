using DocBookAPI.Models;

namespace DocBookAPI.Interfaces
{
    public interface IMedicalReportService
    {
        Task<MedicalReport> CreateMedicalReport(MedicalReport medicalReport);
        Task<MedicalReport> GetMedicalReport(int id);
        Task<IEnumerable<MedicalReport>> GetMedicalReports();
        Task<bool> UpdateMedicalReport(MedicalReport medicalReport);
        Task<bool> DeleteMedicalReport(int id);
        Task<IEnumerable<MedicalReport>> GetMedicalReportsByAppointmentId(int appointmentId);
    }
}
