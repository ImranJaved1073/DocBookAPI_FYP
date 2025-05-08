using AutoMapper;
using DocBookAPI.Data;
using DocBookAPI.DTOs;
using DocBookAPI.Interfaces;
using DocBookAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DocBookAPI.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public AppointmentService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<AppointmentDTO> CreateAppointment(AppointmentDTO appointment)
        {
            var newAppointment = _mapper.Map<Appointment>(appointment);
            _context.Appointments.Add(newAppointment);
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

        public async Task<IEnumerable<Appointment>> GetAppointmentsByDate(int id, DateTime date)
        {
            return await _context.Appointments
                .Where(a => a.DoctorId == id && a.AppointmentDate.Date == date.Date && a.Status != "cancelled")
                .ToListAsync();
        }

        public async Task<Appointment> ChangeAppointmentStatus(int id, string status)
        {
            var appointment = await GetAppointment(id);
            if (appointment == null)
            {
                return null!;
            }
            if(appointment.Status.ToLower() == "approved" && status.ToLower() == "approved")
            {
                return null!;
            }
            appointment.Status = status;
            _context.Appointments.Update(appointment);
            await _context.SaveChangesAsync();
            return appointment;
        }

        public async Task<Appointment> CancelAppointment(int id)
        {
            return await ChangeAppointmentStatus(id, "Cancelled");
        }

        public async Task<Appointment> ApproveAppointment(int id)
        {
            return await ChangeAppointmentStatus(id, "Approved");
        }

        // get book slots of doctor
        //public async Task<IEnumerable<string>> GetBookedSlots(int doctorId, DateTime date)
        //{
        //    var bookedSlots = await _context.Appointments
        //        .Where(a => a.DoctorId == doctorId && a.AppointmentDate.Date == date.Date)
        //        .Select(a => a.BookedSlots)
        //        .ToListAsync();

        //    return bookedSlots;
        //}

        public async Task<IEnumerable<BookedSlotDTO>> GetBookedSlots(int doctorId, DateTime date)
        {
            var bookedSlots = await _context.Appointments
                .Where(a => a.DoctorId == doctorId && a.AppointmentDate.Date == date.Date)
                .Select(a => new BookedSlotDTO
                {
                    Slot = a.BookedSlots,
                    Status = a.Status // assuming a.Status is like "booked" or "pending"
                })
                .ToListAsync();

            return bookedSlots;
        }

        // get booked slots of patient
        public async Task<IEnumerable<BookedSlotDTO>> GetPatientBookedSlots(int patientId, int doctorId, DateTime date)
        {
            return await _context.Appointments
                .Where(a => a.PatientId == patientId && a.DoctorId == doctorId && a.AppointmentDate.Date == date.Date)
                .Select(a => new BookedSlotDTO
                {
                    Slot = a.BookedSlots,
                    Status = a.Status // assuming a.Status is like "booked" or "pending"
                })
                .ToListAsync();
        }


        // get booked slots of doctor
        //public async Task<IEnumerable<string>> GetBookedSlots(int doctorId, DateTime date, string bookedSlots)
        //{
        //    var bookedSlotsList = await _context.Appointments
        //        .Where(a => a.DoctorId == doctorId && a.AppointmentDate.Date == date.Date)
        //        .Select(a => a.BookedSlots)
        //        .ToListAsync();
        //    bookedSlotsList.Add(bookedSlots);
        //    return bookedSlotsList;
        //}
    }
}
