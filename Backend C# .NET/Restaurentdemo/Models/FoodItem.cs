using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurentdemo.Models
{
    public class FoodItem
    {
        
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? FoodItemId { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string FoodItemName { get; set; }

        public bool Veg { get; set; }

        public bool IsAvailable { get; set; }

        public decimal Price { get; set; }
        [BsonIgnore]
        public int Quantity { get; set; }
    }
}
