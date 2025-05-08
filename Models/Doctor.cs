namespace DocBookAPI.Models
{
    public class Doctor
    {
        public int Id { get; set; }
        public string UserId { get; set; }  // References User
        public string? Name { get; set; }
        public string? Hospital{ get; set; } // Nullable, doctors may not belong to a hospital
        public string? Specialization { get; set; }
        public string? Qualification { get; set; }
        public int? ExperienceYears { get; set; }
        public decimal? ConsultationFee { get; set; }
        public string? Availability { get; set; } // JSON string for available time slots
        public string? Bio { get; set; }

        // Navigation properties
        public virtual ApplicationUser User { get; set; }
        public virtual IEnumerable<Appointment> Appointments { get; set; }
        public virtual IEnumerable<Review> Reviews { get; set; }
    }
}
