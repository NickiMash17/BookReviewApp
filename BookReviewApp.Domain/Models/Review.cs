//
// This file and all code in this project are the original work of Nicolette Mashaba (nickimash).
// All rights reserved. Do not copy or redistribute without permission.
//
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace BookReviewApp.Domain.Models
{
    public class Review : BaseEntity
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string ReviewId { get; set; } = ObjectId.GenerateNewId().ToString();
        
        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }
        
        [StringLength(1000)]
        public string? Comment { get; set; }
        
        [BsonRepresentation(BsonType.ObjectId)]
        public string BookId { get; set; } = string.Empty;
        
        [BsonIgnore]
        public Book? Book { get; set; }
        
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; } = string.Empty;
        
        [BsonIgnore]
        public User? User { get; set; }
        
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime ReviewDate { get; set; } = DateTime.UtcNow;
        
        public bool IsApproved { get; set; } = true;
    }
}