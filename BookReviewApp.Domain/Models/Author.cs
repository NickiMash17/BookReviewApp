//
// This file and all code in this project are the original work of Nicolette Mashaba (nickimash).
// All rights reserved. Do not copy or redistribute without permission.
//
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace BookReviewApp.Domain.Models
{
    public class Author : BaseEntity
    {
        // Removed AuthorId property. Use Id from BaseEntity as PK.
        
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = null!;
        
        [StringLength(500)]
        public string? Biography { get; set; }
        
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime DateOfBirth { get; set; }
        
        [StringLength(200)]
        public string? PhotoUrl { get; set; }
        
        [BsonIgnore]
        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}