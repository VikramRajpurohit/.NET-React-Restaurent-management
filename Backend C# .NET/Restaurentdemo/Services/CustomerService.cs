using MongoDB.Driver;
using Microsoft.Extensions.Options;
using Restaurentdemo.Models;

namespace Restaurentdemo.Services
{
    public class CustomerService
    {
        private readonly IMongoCollection<Customer> _CustmrCollection;

        public CustomerService(
            IOptions<RestaurantDBContext> restaurentDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                restaurentDatabaseSettings.Value.ConnectionString);
            var mongoDetabase = mongoClient.GetDatabase(
                restaurentDatabaseSettings.Value.DatabaseName);
            _CustmrCollection = mongoDetabase.GetCollection<Customer>(
                restaurentDatabaseSettings.Value.Customers);
        }

        public async Task<List<Customer>> GetAsync() =>
            await _CustmrCollection.Find(_ => true).ToListAsync();
        public async Task<Customer?> GetAsync(string customerId) =>
           await _CustmrCollection.Find(x => x.customerId == customerId).FirstOrDefaultAsync();

        public async Task<Customer?> GetAsyncName(string customerName) =>
            await _CustmrCollection.Find(x => x.CustomerName.First == customerName).FirstOrDefaultAsync();
        public async Task CreateAsync(Customer customer) =>
            await _CustmrCollection.InsertOneAsync(customer);

        public async Task UpdateAsync( String customerId ,Customer customer) =>
            await _CustmrCollection.ReplaceOneAsync( x => x.customerId == customerId, customer);

        public async Task RemoveAsync (String customerId) =>
            await _CustmrCollection.DeleteOneAsync(x => x.customerId == customerId);

    }
}
