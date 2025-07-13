using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace BookReviewApp.Domain.Models
{
    public class User : BaseEntity
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; } = ObjectId.GenerateNewId().ToString();
        
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
        public bool EmailConfirmed { get; set; } = false;
        
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime? LastLoginDate { get; set; }
        
        [StringLength(200)]
        public string? ProfilePictureUrl { get; set; }
        
        public bool IsAdmin { get; set; } = false;
        
        [BsonIgnore]
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
} 