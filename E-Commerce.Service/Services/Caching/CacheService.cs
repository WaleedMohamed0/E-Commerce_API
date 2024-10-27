using StackExchange.Redis;
using System.Text.Json;

namespace E_Commerce.Service.Services.Caching
{
    public class CacheService : ICacheService
    {
        private readonly IDatabase _database;
        public CacheService(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }
        public async Task<string?> GetCacheKeyAsync(string key)
        {
            var response = await _database.StringGetAsync(key);
            if(response.IsNullOrEmpty) return null;
            
            return response;
        }

        public async Task SetCacheKeyAsync(string key, object value, TimeSpan? expiration = null)
        {
            if (value == null) return;
            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            await _database.StringSetAsync(key, JsonSerializer.Serialize(value, options), expiration);
        }
    }
}
