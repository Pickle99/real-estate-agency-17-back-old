using System;
using System.ComponentModel.DataAnnotations;

namespace real_estate_agency_17_back.ReaService.Api.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(25)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(35)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(45)]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        [MaxLength(32)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{6,32}$",
            ErrorMessage = "Password must contain at least 6 characters, including 1 uppercase letter, 1 lowercase letter, and 1 number.")]
        public string Password { get; set; }

        public DateTime CreatedAt { get; set; } // Timestamp for creation
        public DateTime UpdatedAt { get; set; } // Timestamp for update
    }
}