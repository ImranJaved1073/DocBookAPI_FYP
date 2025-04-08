using DocBookAPI.DTOs;
using DocBookAPI.Models;

namespace DocBookAPI.Interfaces
{
    public interface IPrescriptionService
    {
        Task<PrescriptionDTO> CreatePrescription(PrescriptionDTO prescriptiondto);
        Task<Prescription> GetPrescription(int id);
        Task<IEnumerable<Prescription>> GetPrescriptions();
        Task<bool> UpdatePrescription(Prescription prescription);
        Task<bool> DeletePrescription(int id);
        Task<IEnumerable<Prescription>> GetPrescriptionsByAppointmentId(int appointmentId);
        Task<IEnumerable<Prescription>> GetPrescriptionsByPatientId(int patientId);
        Task<IEnumerable<Prescription>> GetPrescriptionsByDoctorId(int doctorId);
        Task<IEnumerable<Prescription>> GetPrescriptionsByDoctorIdAndPatientId(int doctorId, int patientId);
        Task<Prescription> GetPrescriptionByDoctorIdAndPatientIdAndAppointmentId(int doctorId, int patientId, int appointmentId);
    }
}