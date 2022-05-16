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
        public Name CustomerName { get; set; }


        [BsonElement]
        public string MobileNumber { get; set; }

        [BsonElement]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime Date { get; set; }

    }
    public class Name
    {

        //
        // Summary: First name
        //
        [Display(Name = "First name")]
        [StringLength(150, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        public string First { get; set; }


        //
        // Summary: Last name
        //
        [Display(Name = "Last name")]
        [StringLength(150, ErrorMessage = "The {0} must be at max {1} characters long.")]
        public string Last { get; set; }

    }

}
