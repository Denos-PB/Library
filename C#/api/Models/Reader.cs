// Reader.cs
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace api.Models
{
    public class Reader : IdentityUser
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("name")]
        [Required]
        [StringLength(200)]
        public required string Name { get; set; }

        [BsonElement("email")]
        [Required]
        [StringLength(300)]
        public required string Email { get; set; }

        [BsonElement("books")]
        public virtual required ICollection<Books> Books { get; set; }

        public virtual ICollection<Coment> Coments { get; set; }
    }
}