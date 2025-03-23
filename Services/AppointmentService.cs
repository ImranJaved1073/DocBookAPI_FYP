using DocBookAPI.Data;
using DocBookAPI.Interfaces;
using DocBookAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DocBookAPI.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly ApplicationDbContext _context;

        public AppointmentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Appointment> CreateAppointment(Appointment appointment)
        {
            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();
            return appointment;
        }

        public async Task<Appointment> GetAppointment(int id)
        {
            return (await _context.Appointments.FindAsync(id))!;
        }

        public async Task<IEnumerable<Appointment>> GetAppointments()
        {
            return await _context.Appointments.ToListAsync();
        }

        public async Task<bool> UpdateAppointment(Appointment appointment)
        {
            if (await GetAppointment(appointment.Id) == null)
            {
                return false;
            }
            _context.Appointments.Update(appointment);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Appointment> DeleteAppointment(int id)
        {
            var appointment = await GetAppointment(id);
            if (appointment != null)
            {
                _context.Appointments.Remove(appointment);
                await _context.SaveChangesAsync();
            }
            
            return appointment!;
        }

        public async Task<IEnumerable<Appointment>> GetAppointmentsByDoctor(int doctorId)
        {
            return await _context.Appointments.Where(a => a.DoctorId == doctorId).ToListAsync();
        }

        public async Task<IEnumerable<Appointment>> GetAppointmentsByPatient(int patientId)
        {
            return await _context.Appointments.Where(a => a.PatientId == patientId).ToListAsync();
        }

        public async Task<IEnumerable<Appointment>> GetAppointmentsByDate(DateTime date)
        {
            return await _context.Appointments.Where(a => a.AppointmentDate.Date == date.Date).ToListAsync();
        }

        public async Task<bool> ChangeAppointmentStatus(int id, string status)
        {
            var appointment = await GetAppointment(id);
            if (appointment == null)
            {
                return false;
            }
            appointment.Status = status;
            _context.Appointments.Update(appointment);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CancelAppointment(int id)
        {
            return await ChangeAppointmentStatus(id, "Cancelled");
        }

    }
}
