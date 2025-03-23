namespace DocBookAPI.Models
{
    public class Doctor
    {
        public int Id { get; set; }
        public int UserId { get; set; }  // References User
        public int? HospitalId { get; set; } // Nullable, doctors may not belong to a hospital
        public string Specialization { get; set; }
        public string Qualification { get; set; }
        public int ExperienceYears { get; set; }
        public decimal ConsultationFee { get; set; }
        public string Availability { get; set; } // JSON string for available time slots

        // Navigation properties
        public virtual User User { get; set; }
        public virtual Hospital Hospital { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
