using DocBookAPI.Data;
using DocBookAPI.Interfaces;
using DocBookAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DocBookAPI.Services
{
    public class MedicalReportService : IMedicalReportService
    {
        private readonly ApplicationDbContext _context;

        public MedicalReportService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<MedicalReport> CreateMedicalReport(MedicalReport medicalReport)
        {
            _context.MedicalReports.Add(medicalReport);
            await _context.SaveChangesAsync();
            return medicalReport;
        }

        public async Task<MedicalReport> GetMedicalReport(int id)
        {
            return (await _context.MedicalReports.FindAsync(id))!;
        }

        public async Task<IEnumerable<MedicalReport>> GetMedicalReports()
        {
            return await _context.MedicalReports.ToListAsync();
        }

        public async Task<bool> UpdateMedicalReport(MedicalReport medicalReport)
        {
            if (await GetMedicalReport(medicalReport.Id) == null)
            {
                return false;
            }
            _context.MedicalReports.Update(medicalReport);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteMedicalReport(int id)
        {
            var medicalReport = await GetMedicalReport(id);
            if (medicalReport != null)
            {
                _context.MedicalReports.Remove(medicalReport);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<MedicalReport>> GetMedicalReportsByAppointmentId(int appointmentId)
        {
            return await _context.MedicalReports.Where(mr => mr.AppointmentId == appointmentId).ToListAsync();
        }
    }
}
