using DocBookAPI.Data;
using DocBookAPI.Interfaces;
using DocBookAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DocBookAPI.Services
{
    public class PrescriptionService : IPrescriptionService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public PrescriptionService(ApplicationDbContext context, ILogger<PrescriptionService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Prescription> CreatePrescription(Prescription prescription)
        {
            _context.Prescriptions.Add(prescription);
            await _context.SaveChangesAsync();
            return prescription;
        }

        public async Task<Prescription> GetPrescription(int id)
        {
            return (await _context.Prescriptions.FindAsync(id))!;
        }

        public async Task<IEnumerable<Prescription>> GetPrescriptions()
        {
            return await _context.Prescriptions.ToListAsync();
        }

        public async Task<bool> UpdatePrescription(Prescription prescription)
        {
            if (await GetPrescription(prescription.Id) == null)
            {
                return false;
            }
            _context.Prescriptions.Update(prescription);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeletePrescription(int id)
        {
            var prescription = await GetPrescription(id);
            if (prescription != null)
            {
                _context.Prescriptions.Remove(prescription);
                await _context.SaveChangesAsync();
            }

            return prescription != null;
        }

        public async Task<IEnumerable<Prescription>> GetPrescriptionsByAppointmentId(int appointmentId)
        {
            return await _context.Prescriptions.Where(p => p.AppointmentId == appointmentId).ToListAsync();
        }

        public async Task<IEnumerable<Prescription>> GetPrescriptionsByPatientId(int patientId)
        {
            return await _context.Prescriptions.Where(p => p.Appointment.PatientId == patientId).ToListAsync();
        }

        public async Task<IEnumerable<Prescription>> GetPrescriptionsByDoctorId(int doctorId)
        {
            return await _context.Prescriptions.Where(p => p.Appointment.DoctorId == doctorId).ToListAsync();
        }

        public async Task<IEnumerable<Prescription>> GetPrescriptionsByDoctorIdAndPatientId(int doctorId, int patientId)
        {
            return await _context.Prescriptions.Where(p => p.Appointment.DoctorId == doctorId && p.Appointment.PatientId == patientId).ToListAsync();
        }

        public async Task<Prescription> GetPrescriptionByDoctorIdAndPatientIdAndAppointmentId(int doctorId, int patientId, int appointmentId)
        {
            return (await _context.Prescriptions.FirstOrDefaultAsync(p => p.Appointment.DoctorId == doctorId && p.Appointment.PatientId == patientId && p.AppointmentId == appointmentId))!;
        }
    }
}
