//
// This file and all code in this project are the original work of Nicolette Mashaba (nickimash).
// All rights reserved. Do not copy or redistribute without permission.
//
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace BookReviewApp.Domain.Models
{
    /// <summary>
    /// Represents a review for a book.
    /// </summary>
    public class Review : BaseEntity
    {
        /// <summary>
        /// The unique identifier for the review.
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        public string ReviewId { get; set; } = ObjectId.GenerateNewId().ToString();
        
        /// <summary>
        /// The rating given in the review (1-5).
        /// </summary>
        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }
        
        /// <summary>
        /// The comment or feedback provided in the review.
        /// </summary>
        [StringLength(1000)]
        public string? Comment { get; set; }
        
        /// <summary>
        /// The ID of the book being reviewed.
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        public string BookId { get; set; } = string.Empty;
        
        /// <summary>
        /// The book entity associated with the review.
        /// </summary>
        [BsonIgnore]
        public Book? Book { get; set; }
        
        /// <summary>
        /// The ID of the user who wrote the review.
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; } = string.Empty;
        
        /// <summary>
        /// The user entity who wrote the review.
        /// </summary>
        [BsonIgnore]
        public User? User { get; set; }
        
        /// <summary>
        /// The date the review was written.
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime ReviewDate { get; set; } = DateTime.UtcNow;
        
        /// <summary>
        /// Indicates whether the review is approved for display.
        /// </summary>
        public bool IsApproved { get; set; } = true;
    }
}