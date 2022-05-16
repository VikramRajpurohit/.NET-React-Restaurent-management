using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Restaurentdemo.Models;

namespace Restaurentdemo.Services
{
    public class OrderDetailService
    {
        private readonly IMongoCollection<OrderDetail> _order;

        public OrderDetailService(
            IOptions<RestaurantDBContext> restaurantDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                restaurantDatabaseSettings.Value.ConnectionString);
            var mongoDetabase = mongoClient.GetDatabase(
                restaurantDatabaseSettings.Value.DatabaseName);
            _order = mongoDetabase.GetCollection<OrderDetail>(
                restaurantDatabaseSettings.Value.OrderDetails);
        }

        public async Task<List<OrderDetail>> GetAsync()=>
            await _order.Find(_ => true).ToListAsync();

        public async Task<OrderDetail> GetAsync(String orderId) =>
            await _order.Find(x => x.OrderId == orderId).FirstOrDefaultAsync();

        public async Task CreateAsync(OrderDetail order) =>
            await _order.InsertOneAsync(order);

        public async Task UpdateAsync(String orderId, OrderDetail order)=>
            await _order.ReplaceOneAsync(x => x.OrderId == orderId, order);

        public async Task RemoveAsync(String orderId) =>
            await _order.DeleteOneAsync(x => x.OrderId==orderId);

    }
}
