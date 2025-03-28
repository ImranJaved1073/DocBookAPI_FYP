using DocBookAPI.Models;

namespace DocBookAPI.DTOs
{
    public class AppointmentDTO
    {
        public int Id { get; set; }

        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Status { get; set; } // Pending, Approved, Cancelled, Completed
        public string Notes { get; set; }
    }
}
