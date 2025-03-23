namespace DocBookAPI.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public int UserId { get; set; }  // References User

        public string MedicalHistory { get; set; }

        // Navigation properties
        public virtual User User { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ICollection<MedicalReport> MedicalReports { get; set; }
        public virtual ICollection<Prescription> Prescriptions { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
