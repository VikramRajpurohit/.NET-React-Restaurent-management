using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Restaurentdemo.Models;

namespace Restaurentdemo.Services
{
    public class FoodItemService
    {
        private readonly IMongoCollection<FoodItem> _fooditems;

        public FoodItemService(
            IOptions<RestaurantDBContext> restaurebntDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                restaurebntDatabaseSettings.Value.ConnectionString);
            var mongoDetabase = mongoClient.GetDatabase(
                restaurebntDatabaseSettings.Value.DatabaseName);
            _fooditems = mongoDetabase.GetCollection<FoodItem>(
                restaurebntDatabaseSettings.Value.FoodItems);
        }

        public async Task<List<FoodItem>> GetAsync()=>
            await _fooditems.Find(_ => true).ToListAsync();

        public async Task<FoodItem> getAsync(String FoodItemId) =>
            await _fooditems.Find(x => x.FoodItemId == FoodItemId).FirstOrDefaultAsync();

        public async Task<FoodItem> getAsyncName(String itmName) =>
            await _fooditems.Find(x => x.FoodItemName == itmName).FirstOrDefaultAsync();

        public async Task CreateAsync(FoodItem itm) =>
            await _fooditems.InsertOneAsync(itm);

        public async Task UpdateAsync(String FoodItemId, FoodItem itm) =>
            await _fooditems.ReplaceOneAsync(x => x.FoodItemId == FoodItemId, itm);

        public async Task RemoveAsync(String FoodItemId) =>
            await _fooditems.DeleteOneAsync(x => x.FoodItemId == FoodItemId);

    }
}
