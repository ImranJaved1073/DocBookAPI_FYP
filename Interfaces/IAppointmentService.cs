using DocBookAPI.DTOs;
using DocBookAPI.Models;

namespace DocBookAPI.Interfaces
{
    public interface IAppointmentService
    {
        Task<AppointmentDTO> CreateAppointment(AppointmentDTO appointment);
        Task<Appointment> GetAppointment(int id);
        Task<IEnumerable<Appointment>> GetAppointments();
        Task<bool> UpdateAppointment(Appointment appointment);
        Task<Appointment> DeleteAppointment(int id);
        Task<IEnumerable<Appointment>> GetAppointmentsByDoctor(int doctorId);
        Task<IEnumerable<Appointment>> GetAppointmentsByPatient(int patientId);
        Task<IEnumerable<Appointment>> GetAppointmentsByDate(int id, DateTime date);
        Task<Appointment> ChangeAppointmentStatus(int id, string status);
        Task<Appointment> CancelAppointment(int id);
        Task<Appointment> ApproveAppointment(int id);
        Task<IEnumerable<BookedSlotDTO>> GetBookedSlots(int doctorId, DateTime date);
        Task<IEnumerable<BookedSlotDTO>> GetPatientBookedSlots(int patientId,int doctorId, DateTime date); 

    }
}
