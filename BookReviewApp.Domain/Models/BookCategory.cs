using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace BookReviewApp.Domain.Models
{
    public class BookCategory : BaseEntity
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string BookCategoryId { get; set; } = ObjectId.GenerateNewId().ToString();
        
        [BsonRepresentation(BsonType.ObjectId)]
        public string BookId { get; set; } = string.Empty;
        
        [BsonRepresentation(BsonType.ObjectId)]
        public string CategoryId { get; set; } = string.Empty;
        
        [BsonIgnore]
        public Book? Book { get; set; }
        
        [BsonIgnore]
        public Category? Category { get; set; }
    }
}
