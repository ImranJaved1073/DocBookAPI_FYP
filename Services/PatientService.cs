using AutoMapper;
using DocBookAPI.Data;
using DocBookAPI.DTOs;
using DocBookAPI.Interfaces;
using DocBookAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DocBookAPI.Services
{
    public class PatientService : IPatientService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public PatientService(ApplicationDbContext context , IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PatientDTO> AddPatientAsync(PatientDTO patient)
        {
            var patientDTO = _mapper.Map<PatientDTO, Patient>(patient);
            await _context.Patients.AddAsync(patientDTO);
            await _context.SaveChangesAsync();
            return patient;
        }

        public async Task<bool> DeletePatientAsync(int id)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient != null)
            {
                _context.Patients.Remove(patient);
                await _context.SaveChangesAsync();
                return true;
            }
            
            return false;
        }

        public async Task<IEnumerable<Patient>> GetAllPatientsAsync()
        {
            return await _context.Patients.ToListAsync();
        }

        public async Task<Patient> GetPatientByEmailAsync(string email)
        {
            return (await _context.Patients.FirstOrDefaultAsync(p => p.User.Email == email))!;
        }

        public async Task<Patient> GetPatientByIdAsync(int id)
        {
            return (await _context.Patients.FindAsync(id))!;
        }

        public async Task<Patient> GetPatientByUserNameAsync(string userName)
        {
            return (await _context.Patients.FirstOrDefaultAsync(p => p.User.UserName == userName))!;
        }
        public async Task<PatientDTO> UpdatePatientAsync(PatientDTO patient)
        {
            var patientDTO = _mapper.Map<PatientDTO, Patient>(patient);
            _context.Patients.Update(patientDTO);
            await _context.SaveChangesAsync();
            return patient;
        }
    }
}
