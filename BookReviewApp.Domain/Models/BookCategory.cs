using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace BookReviewApp.Domain.Models
{
    /// <summary>
    /// Represents the relationship between a book and a category.
    /// </summary>
    public class BookCategory : BaseEntity
    {
        /// <summary>
        /// The unique identifier for the book-category relationship.
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        public string BookCategoryId { get; set; } = ObjectId.GenerateNewId().ToString();
        
        /// <summary>
        /// The ID of the book in the relationship.
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        public string BookId { get; set; } = string.Empty;
        
        /// <summary>
        /// The ID of the category in the relationship.
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        public string CategoryId { get; set; } = string.Empty;
        
        /// <summary>
        /// The book entity in the relationship.
        /// </summary>
        [BsonIgnore]
        public Book? Book { get; set; }
        
        /// <summary>
        /// The category entity in the relationship.
        /// </summary>
        [BsonIgnore]
        public Category? Category { get; set; }
    }
}
