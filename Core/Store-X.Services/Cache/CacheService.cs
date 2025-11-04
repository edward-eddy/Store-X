using Store_X.Domain.Contracts;
using Store_X.Services_Abstractions.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store_X.Services.Cache
{
    public class CacheService(ICacheRepository _cacheRepository) : ICacheService
    {
        public async Task<string> GetAsync(string key)
        {
            return await _cacheRepository.GetAsync(key);
        }

        public async Task SetAsync(string key, object value, TimeSpan lifeTime)
        {
            await _cacheRepository.SetAsync(key, value, lifeTime);
        }
    }
}
