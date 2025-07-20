using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace BookReviewApp.Domain.Models
{
    /// <summary>
    /// Represents a user of the application.
    /// </summary>
    public class User : BaseEntity
    {
        /// <summary>
        /// The unique identifier for the user.
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; } = ObjectId.GenerateNewId().ToString();
        
        /// <summary>
        /// The email address of the user.
        /// </summary>
        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; } = null!;
        
        /// <summary>
        /// The username of the user.
        /// </summary>
        [Required]
        [StringLength(100)]
        public string Username { get; set; } = null!;
        
        /// <summary>
        /// The hashed password of the user.
        /// </summary>
        [Required]
        [StringLength(100)]
        public string PasswordHash { get; set; } = null!;
        
        /// <summary>
        /// The first name of the user.
        /// </summary>
        [StringLength(100)]
        public string? FirstName { get; set; }
        
        /// <summary>
        /// The last name of the user.
        /// </summary>
        [StringLength(100)]
        public string? LastName { get; set; }
        
        /// <summary>
        /// The role of the user (e.g., Admin, User).
        /// </summary>
        [StringLength(20)]
        public string Role { get; set; } = "User"; // Admin, User
        
        /// <summary>
        /// Indicates whether the user account is active.
        /// </summary>
        public bool IsActive { get; set; } = true;
        
        /// <summary>
        /// Indicates whether the user's email is confirmed.
        /// </summary>
        public bool EmailConfirmed { get; set; } = false;
        
        /// <summary>
        /// The date and time of the user's last login.
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime? LastLoginDate { get; set; }
        
        /// <summary>
        /// The URL of the user's profile picture.
        /// </summary>
        [StringLength(200)]
        public string? ProfilePictureUrl { get; set; }
        
        /// <summary>
        /// Indicates whether the user is an admin.
        /// </summary>
        public bool IsAdmin { get; set; } = false;
        
        /// <summary>
        /// The collection of reviews written by the user.
        /// </summary>
        [BsonIgnore]
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
} 