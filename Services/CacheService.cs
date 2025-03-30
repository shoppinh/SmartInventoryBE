using Microsoft.Extensions.Caching.Memory;
using SmartInventoryBE.Interfaces.Services;

namespace SmartInventoryBE.Services
{
    public class CacheService : ICacheService
    {
        private readonly IMemoryCache _cache;
        private readonly MemoryCacheEntryOptions _options;

        public CacheService(IMemoryCache cache)
        {
            _cache = cache;
            _options = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10),
                SlidingExpiration = TimeSpan.FromMinutes(2)
            };
        }

        public async Task<T?> GetOrCreateAsync<T>(string key, Func<Task<T>> factory)
        {
            if (!_cache.TryGetValue(key, out T? value))
            {
                value = await factory();
                _cache.Set(key, value, _options);
            }
            return value;
        }

        public void Remove(string key)
        {
            _cache.Remove(key);
        }
    }
}
