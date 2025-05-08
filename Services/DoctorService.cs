using AutoMapper;
using DocBookAPI.Data;
using DocBookAPI.DTOs;
using DocBookAPI.Interfaces;
using DocBookAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DocBookAPI.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public DoctorService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<DoctorDTO> AddDoctorAsync(DoctorDTO doctorDTO)
        {
            var doctor = _mapper.Map<DoctorDTO, Doctor>(doctorDTO);
            await _context.Doctors.AddAsync(doctor);
            await _context.SaveChangesAsync();
            return doctorDTO;
        }

        public async Task<bool> DeleteDoctorAsync(int id)
        {
            var doctor = await _context.Doctors.FindAsync(id);
            if (doctor != null)
            {
                _context.Doctors.Remove(doctor);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<IEnumerable<Doctor>> GetAllDoctorsAsync()
        {
            return await _context.Doctors.Where(d => d.Availability != null && d.ConsultationFee != null
                && d.Specialization != null && d.Qualification != null && 
                d.Availability != null  && d.ExperienceYears != null).ToListAsync();
        }

        public async Task<Doctor> GetDoctorByEmailAsync(string email)
        {
            return (await _context.Doctors.FirstOrDefaultAsync(d => d.User.Email == email))!;
        }

        public async Task<Doctor> GetDoctorByIdAsync(int id)
        {
            return (await _context.Doctors.FindAsync(id))!;
        }

        public async Task<Doctor> GetDoctorByUserNameAsync(string userName)
        {
            return (await _context.Doctors.FirstOrDefaultAsync(d => d.User.UserName == userName))!;
        }

        public async Task<DoctorDTO> UpdateDoctorAsync(DoctorDTO doctor)
        {
            var doctorDTO = _mapper.Map<DoctorDTO, Doctor>(doctor);
            _context.Doctors.Update(doctorDTO);
            await _context.SaveChangesAsync();
            return doctor;
        }

        public async Task<DoctorDTO> UpdateDoctorAsync(int id, DoctorDTO doctorDto)
        {
            var existingDoctor = await _context.Doctors.FirstOrDefaultAsync(d => d.Id == id);

            var doctor = _mapper.Map<DoctorDTO, Doctor>(doctorDto);

            if (existingDoctor != null)
            {
                existingDoctor.Name = doctor.Name;
                existingDoctor.Bio = doctor.Bio;
                existingDoctor.Hospital = doctor.Hospital;
                existingDoctor.Specialization = doctor.Specialization;
                existingDoctor.Qualification = doctor.Qualification;
                existingDoctor.ExperienceYears = doctor.ExperienceYears;
                existingDoctor.ConsultationFee = doctor.ConsultationFee;
                existingDoctor.Availability = doctor.Availability;
                _context.Doctors.Update(existingDoctor);
                await _context.SaveChangesAsync();
            }

            return doctorDto;
        }
    }
}
