namespace Restaurentdemo.Models
{
    public class RestaurantDBContext
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;

        public string Customers { get; set; } = null!;

        public string FoodItems { get; set; } = null!;

        public string OrderMasters { get; set; } = null!;

        public string OrderDetails { get; set; } = null!;

    }
}
