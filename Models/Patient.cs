﻿namespace DocBookAPI.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public string UserId { get; set; }  // References User

        public string? MedicalHistory { get; set; }

        public int? Weight { get; set; }

        public int? Height { get; set; }


        // Navigation properties
        public virtual ApplicationUser User { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ICollection<MedicalReport> MedicalReports { get; set; }
        public virtual ICollection<Prescription> Prescriptions { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
