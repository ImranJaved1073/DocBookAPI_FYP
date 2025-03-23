namespace DocBookAPI.Models
{
    public class Prescription
    {
        public int Id { get; set; }

        public int AppointmentId { get; set; }
        public string Medicines { get; set; } // Comma separated medicine names
        public int Frequency { get; set; } 
        public string AdditionalInstructions { get; set; }
        public DateTime IssuedDate { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public virtual Appointment Appointment { get; set; }
    }
}
