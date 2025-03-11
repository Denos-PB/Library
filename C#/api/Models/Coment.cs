using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace api.Models
{
    public class Coment
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("text")]
        [Required]
        [StringLength(10000)]
        public required string Text { get; set; }

        [BsonElement("bookId")]
        public string? BookId { get; set; }

        [BsonElement("readerName")]
        [Required]
        [StringLength(200)]
        public string? ReaderName { get; set; }

        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}