using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurentdemo.Models
{
    public class OrderMaster
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public String OrderMasterId { get; set; }
        [Column(TypeName="nvarchar(75)")]
        public String OrderNo { get; set; }
        
        public String customerId { get; set; }

        public Customer Customer { get; set; }

        [Column(TypeName = "nvarchar(10)")]
        public String PaymentMethod { get; set; }

        public decimal GTotal { get; set; } 

        public List<OrderDetail> OrderDetails { get; set; }

        [NotMapped]
        public String DeletedOrderItemIds { get; set; } 


    }
}
