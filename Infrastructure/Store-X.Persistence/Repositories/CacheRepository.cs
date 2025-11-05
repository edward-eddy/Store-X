using StackExchange.Redis;
using Store_X.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Store_X.Persistence.Repositories
{
    public class CacheRepository(IConnectionMultiplexer connectionMultiplexer) : ICacheRepository
    {
        private readonly IDatabase _database = connectionMultiplexer.GetDatabase();
        public async Task<string> GetAsync(string key)
        {
            var redisValue = await _database.StringGetAsync(key);
            return redisValue;
        }

        public async Task SetAsync(string key, object value, TimeSpan lifeTime)
        {
            await _database.StringSetAsync(key, JsonSerializer.Serialize(value), lifeTime);
        }
    }
}
