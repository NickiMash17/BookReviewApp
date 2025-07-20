using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BookReviewApp.Domain.Models
{
    /// <summary>
    /// Represents the base entity for all models, providing common properties.
    /// </summary>
    public abstract class BaseEntity
    {
        /// <summary>
        /// The unique identifier for the entity.
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();
        
        /// <summary>
        /// The date and time the entity was created.
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        /// <summary>
        /// The date and time the entity was last updated.
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime? UpdatedAt { get; set; }
    }
}