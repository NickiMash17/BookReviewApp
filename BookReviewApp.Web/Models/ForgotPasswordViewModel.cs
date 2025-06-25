using System.ComponentModel.DataAnnotations;

namespace BookReviewApp.Web.Models
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;
    }
} 