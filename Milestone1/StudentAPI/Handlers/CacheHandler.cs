using Microsoft.Extensions.Caching.Memory;
using StudentAPI.Handlers.Interface;

namespace StudentAPI.Handlers
{
    public class CacheHandler : Handler,ICacheHandler
    {
        private IMemoryCache _memoryCache;

        public CacheHandler(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public async override Task<DateTime?> Handle(int roll)
        {
            var cacheData = _memoryCache.Get<DateTime?>(Convert.ToString(roll));
            if (cacheData != null)
            {
                return await Task.FromResult(cacheData);
            }
            return await HandleNext(roll);
        }
    }
}
