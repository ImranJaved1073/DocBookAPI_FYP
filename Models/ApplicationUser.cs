using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace DocBookAPI.Models
{
    public class ApplicationUser : IdentityUser
    {

        [Required]
        public string Gender { get; set; } = null!;

        public DateTime? DateOfBirth { get; set; } // Nullable in database

        public string? Address { get; set; } // Nullable in database

        public string? ProfilePicture { get; set; } // Nullable in database

        public virtual Patient Patient { get; set; }
        public virtual Doctor Doctor { get; set; }
    }

}
