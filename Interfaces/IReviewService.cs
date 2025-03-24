using DocBookAPI.Models;

namespace DocBookAPI.Interfaces
{
    public interface IReviewService
    {
        Task<Review> CreateReview(Review review);
        Task<Review> GetReview(int id);
        Task<IEnumerable<Review>> GetReviews();
        Task<bool> UpdateReview(Review review);
        Task<bool> DeleteReview(int id);
        Task<IEnumerable<Review>> GetReviewsByDoctorId(int doctorId);
        Task<IEnumerable<Review>> GetReviewsByPatientId(int patientId);
        Task<IEnumerable<Review>> GetReviewsByDoctorIdAndPatientId(int doctorId, int patientId);
    }
}
