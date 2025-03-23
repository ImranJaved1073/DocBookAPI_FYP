namespace DocBookAPI.Models
{
    public class MedicalReport
    {
        public int Id { get; set; }

        public int AppointmentId { get; set; }
        public string ReportFile { get; set; } // File path
        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
        public string HealthDescription { get; set; } // Patient can describe their illness or symptoms

        // Navigation properties
        public virtual Appointment Appointment { get; set; }
    }
}
