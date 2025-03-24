using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DocBookAPI.Models
{
    public class Review
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int PatientId { get; set; } // Foreign Key to Patients

        public int DoctorId { get; set; } // Foreign Key to Doctors

        [Range(1, 5)]
        public int Rating { get; set; } // Rating between 1 and 5

        public string ReviewText { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        public virtual Patient Patient { get; set; }
        public virtual Doctor Doctor { get; set; }
    }
}
