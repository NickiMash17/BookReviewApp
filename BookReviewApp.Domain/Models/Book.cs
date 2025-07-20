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
    /// Represents a book in the system.
    /// </summary>
    public class Book : BaseEntity
    {
        /// <summary>
        /// The title of the book.
        /// </summary>
        [Required]
        [StringLength(100)]
        public string Title { get; set; } = null!;

        /// <summary>
        /// The description of the book.
        /// </summary>
        [StringLength(1000)]
        public string? Description { get; set; }

        /// <summary>
        /// The ISBN number of the book.
        /// </summary>
        [StringLength(20)]
        public string? ISBN { get; set; }

        /// <summary>
        /// The date the book was published.
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime PublishedDate { get; set; }

        /// <summary>
        /// The price of the book.
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive number")]
        public decimal Price { get; set; }

        /// <summary>
        /// The URL of the book's cover image.
        /// </summary>
        [StringLength(200)]
        public string? CoverImageUrl { get; set; }

        /// <summary>
        /// The ID of the author who wrote the book.
        /// </summary>
        [Required]
        [BsonRepresentation(BsonType.ObjectId)]
        public string AuthorId { get; set; } = string.Empty; // FK to Author.Id

        /// <summary>
        /// The author entity associated with the book.
        /// </summary>
        [BsonIgnore]
        public Author? Author { get; set; }

        /// <summary>
        /// The collection of reviews for the book.
        /// </summary>
        [BsonIgnore]
        public ICollection<Review> Reviews { get; set; } = new List<Review>();

        /// <summary>
        /// The collection of categories associated with the book.
        /// </summary>
        [BsonIgnore]
        public ICollection<BookCategory> BookCategories { get; set; } = new List<BookCategory>();
    }
}