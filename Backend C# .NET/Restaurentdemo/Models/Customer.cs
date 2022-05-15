using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Text.Json.Serialization;

namespace Restaurentdemo.Models
{
    public class Customer
    {
        
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? customerId { get; set; }

        
        [Column(TypeName ="navchar(100")]
        public string CustomerName { get; set; }
    }
}
