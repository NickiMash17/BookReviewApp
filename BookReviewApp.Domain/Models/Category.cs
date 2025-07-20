using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace BookReviewApp.Domain.Models
{
    /// <summary>
    /// Represents a category for books.
    /// </summary>
    public class Category : BaseEntity
    {
        /// <summary>
        /// The unique identifier for the category.
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        public string CategoryId { get; set; } = ObjectId.GenerateNewId().ToString();
        
        /// <summary>
        /// The name of the category.
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Name { get; set; } = null!;
        
        /// <summary>
        /// The description of the category.
        /// </summary>
        [StringLength(200)]
        public string? Description { get; set; }
        
        /// <summary>
        /// The collection of book-category relationships for this category.
        /// </summary>
        [BsonIgnore]
        public ICollection<BookCategory> BookCategories { get; set; } = new List<BookCategory>();
    }
}
