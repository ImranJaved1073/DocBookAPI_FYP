namespace DocBookAPI.Models
{
    public class Appointment
    {
        public int Id { get; set; }

        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Status { get; set; } // Pending, Approved, Cancelled, Completed
        public string BookedSlots { get; set; } // comma seperated slots
        public string Notes { get; set; }

        // Navigation properties

    }
}
