using DocBookAPI.Models;

namespace DocBookAPI.Interfaces
{
    public interface IAppointmentService
    {
        Task<Appointment> CreateAppointment(Appointment appointment);
        Task<Appointment> GetAppointment(int id);
        Task<IEnumerable<Appointment>> GetAppointments();
        Task<bool> UpdateAppointment(Appointment appointment);
        Task<Appointment> DeleteAppointment(int id);
        Task<IEnumerable<Appointment>> GetAppointmentsByDoctor(int doctorId);
        Task<IEnumerable<Appointment>> GetAppointmentsByPatient(int patientId);
        Task<IEnumerable<Appointment>> GetAppointmentsByDate(DateTime date);
        Task<bool> ChangeAppointmentStatus(int id, string status);
        Task<bool> CancelAppointment(int id);

    }
}
