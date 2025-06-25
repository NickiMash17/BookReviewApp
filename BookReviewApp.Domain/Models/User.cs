using System.ComponentModel.DataAnnotations;

namespace BookReviewApp.Domain.Models
{
    public class User : BaseEntity
    {
        [Key]
        public int UserId { get; set; }
        
        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; } = null!;
        
        [Required]
        [StringLength(100)]
        public string Username { get; set; } = null!;
        
        [Required]
        [StringLength(100)]
        public string PasswordHash { get; set; } = null!;
        
        [StringLength(100)]
        public string? FirstName { get; set; }
        
        [StringLength(100)]
        public string? LastName { get; set; }
        
        [StringLength(20)]
        public string Role { get; set; } = "User"; // Admin, User
        
        public bool IsActive { get; set; } = true;
        
        public DateTime? LastLoginDate { get; set; }
        
        [StringLength(200)]
        public string? ProfilePictureUrl { get; set; }
        
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
} 