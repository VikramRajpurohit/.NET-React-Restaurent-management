using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Restaurentdemo.Models
{
    public class OrderDetail
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? OrderId { get; set; } 

        public List<FoodOrderItem> FoodOrderItems { get; set; }

    }

    public class FoodOrderItem
    {
        public string FoodItemId { get; set; }
        public int Quantity { get; set; }
    }

    public class OrderDetailResponse
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? OrderId { get; set; }

        public List<FoodItem> FoodOrderItems { get; set; }  

    }
}
