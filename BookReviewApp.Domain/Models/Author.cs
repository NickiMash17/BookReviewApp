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
    /// Represents an author in the system.
    /// </summary>
    public class Author : BaseEntity
    {
        /// <summary>
        /// The name of the author.
        /// </summary>
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = null!;
        
        /// <summary>
        /// The biography of the author.
        /// </summary>
        [StringLength(500)]
        public string? Biography { get; set; }
        
        /// <summary>
        /// The date of birth of the author.
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime DateOfBirth { get; set; }
        
        /// <summary>
        /// The URL of the author's photo.
        /// </summary>
        [StringLength(200)]
        public string? PhotoUrl { get; set; }
        
        /// <summary>
        /// The collection of books written by the author.
        /// </summary>
        [BsonIgnore]
        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}