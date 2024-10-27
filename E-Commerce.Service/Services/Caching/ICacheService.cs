using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Service.Services.Caching
{
    public interface ICacheService
    {
        Task SetCacheKeyAsync(string key, object value, TimeSpan? expiration = null);
        Task<string?> GetCacheKeyAsync(string key);
    }
}
