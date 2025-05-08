namespace DocBookAPI.DTOs
{
    public class DoctorDTO
    {
        public int Id { get; set; }
        public string UserId { get; set; }  // References User
        public string? Name { get; set; }
        public string? Bio { get; set; }
        public string? Hospital { get; set; } // Nullable, doctors may not belong to a hospital
        public string? Specialization { get; set; }
        public string? Qualification { get; set; }
        public int? ExperienceYears { get; set; }
        public decimal? ConsultationFee { get; set; }
        public string? Availability { get; set; } // JSON string for available time slots
    }
}
