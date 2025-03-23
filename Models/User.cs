using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace DocBookAPI.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string UserName { get; set; } = null!;

        [Required, EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        public string PasswordHash { get; set; } = null!;

        public string? Phone { get; set; } // Nullable in database

        [Required]
        public string Gender { get; set; } = null!;

        public DateTime? DateOfBirth { get; set; } // Nullable in database

        public string? Address { get; set; } // Nullable in database

        public string? ProfilePicture { get; set; } // Nullable in database

        [Required]
        public string Role { get; set; } = null!;

        public virtual Patient Patient { get; set; }
        public virtual Doctor Doctor { get; set; }
    }

}
