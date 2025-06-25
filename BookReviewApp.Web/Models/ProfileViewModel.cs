using System.ComponentModel.DataAnnotations;

namespace BookReviewApp.Web.Models
{
    public class ProfileViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Username { get; set; } = null!;

        [StringLength(100)]
        public string? FirstName { get; set; }

        [StringLength(100)]
        public string? LastName { get; set; }

        public string Role { get; set; } = "User";

        public string? ProfilePictureUrl { get; set; }
    }
} 