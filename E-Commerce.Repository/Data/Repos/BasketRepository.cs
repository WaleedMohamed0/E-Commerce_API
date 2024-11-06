using E_Commerce.Core.DTOs;
using E_Commerce.Core.Models;
using StackExchange.Redis;
using System.Text.Json;

namespace E_Commerce.Repository.Data.Repos
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;
        public BasketRepository(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }
        public async Task<bool> DeleteBasketAsync(string basketId)
        {
            return await _database.KeyDeleteAsync(basketId);
        }

        public async Task<CustomerBasketDTO?> GetBasketAsync(string basketId)
        {
            var data = await _database.StringGetAsync(basketId);
            return data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasketDTO>(data);
        }

        public async Task<CustomerBasketDTO?> UpdateBasketAsync(CustomerBasketDTO basket)
        {
            var created = await _database.StringSetAsync(basket.Id, JsonSerializer.Serialize(basket), TimeSpan.FromDays(30));
            return created is false ? null : await GetBasketAsync(basket.Id);
        }
    }
}
