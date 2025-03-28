// Books.cs
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace api.Models
{
    public class Books
    {   
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [Required]
        [StringLength(200)]
        [BsonElement("title")]
        public required string Title { get; set; }

        [Required]
        [StringLength(100)]
        [BsonElement("genre")]
        public required string Genre { get; set; }

        [Required]
        [Range(1, 20)]
        [BsonElement("count")]
        public int Count { get; set; }

        [Required]
        [StringLength(200)]
        [BsonElement("authorName")]
        public required string AuthorName { get; set; }

        [BsonElement("comments")]
        public List<Coment>? Comments { get; set; }

        [BsonElement("readerId")]
        public string? ReaderId { get; set; }

        [BsonElement("reader")]
        public virtual Reader? Reader { get; set; }

        [BsonElement("authorId")]
        public string? AuthorId { get; set; }

        [BsonElement("author")]
        public virtual Author? Author { get; set; }  
    }
}