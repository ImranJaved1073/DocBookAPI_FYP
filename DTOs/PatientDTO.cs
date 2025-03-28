namespace DocBookAPI.DTOs
{
    public class PatientDTO
    {
        public int Id { get; set; }
        public string UserId { get; set; }  // References User

        public string? MedicalHistory { get; set; }

        public int? Weight { get; set; }

        public int? Height { get; set; }
    }
}
