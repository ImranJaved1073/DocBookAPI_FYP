using DocBookAPI.Data;
using DocBookAPI.Interfaces;
using DocBookAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DocBookAPI.Services
{
    public class ReviewService : IReviewService
    {
        private readonly ApplicationDbContext _context;
        public ReviewService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Review> CreateReview(Review review)
        {
            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();
            return review;
        }
        public async Task<Review> GetReview(int id)
        {
            return (await _context.Reviews.FindAsync(id))!;
        }
        public async Task<IEnumerable<Review>> GetReviews()
        {
            return await _context.Reviews.ToListAsync();
        }
        public async Task<bool> UpdateReview(Review review)
        {
            if (await GetReview(review.Id) == null)
            {
                return false;
            }
            _context.Reviews.Update(review);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteReview(int id)
        {
            var review = await GetReview(id);
            if (review != null)
            {
                _context.Reviews.Remove(review);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<IEnumerable<Review>> GetReviewsByDoctorId(int doctorId)
        {
            return await _context.Reviews.Where(r => r.DoctorId == doctorId).ToListAsync();
        }
        public async Task<IEnumerable<Review>> GetReviewsByPatientId(int patientId)
        {
            return await _context.Reviews.Where(r => r.PatientId == patientId).ToListAsync();
        }
        public async Task<IEnumerable<Review>> GetReviewsByDoctorIdAndPatientId(int doctorId, int patientId)
        {
            return await _context.Reviews.Where(r => r.DoctorId == doctorId && r.PatientId == patientId).ToListAsync();
        }
    }
}
