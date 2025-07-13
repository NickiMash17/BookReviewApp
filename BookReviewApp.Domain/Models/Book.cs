//
// This file and all code in this project are the original work of Nicolette Mashaba (nickimash).
// All rights reserved. Do not copy or redistribute without permission.
//
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace BookReviewApp.Domain.Models
{
    public class Book : BaseEntity
    {
        // Removed BookId property. Use Id from BaseEntity as PK.
        
        [Required]
        [StringLength(100)]
        public string Title { get; set; } = null!;
        
        [StringLength(1000)]
        public string? Description { get; set; }
        
        [StringLength(20)]
        public string? ISBN { get; set; }
        
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime PublishedDate { get; set; }
        
        [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive number")]
        public decimal Price { get; set; }
        
        [StringLength(200)]
        public string? CoverImageUrl { get; set; }
        
        [Required]
        [BsonRepresentation(BsonType.ObjectId)]
        public string AuthorId { get; set; } = string.Empty; // FK to Author.Id
        
        [BsonIgnore]
        public Author? Author { get; set; }
        
        [BsonIgnore]
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        
        [BsonIgnore]
        public ICollection<BookCategory> BookCategories { get; set; } = new List<BookCategory>();
    }
}