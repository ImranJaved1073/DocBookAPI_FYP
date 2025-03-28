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

        public async Task<DoctorDTO> AddDoctorAsync(DoctorDTO doctor)
        {
            var doctorDTO = _mapper.Map<DoctorDTO, Doctor>(doctor);
            await _context.Doctors.AddAsync(doctorDTO);
            await _context.SaveChangesAsync();
            return doctor;
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
            return await _context.Doctors.ToListAsync();
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
    }
}
