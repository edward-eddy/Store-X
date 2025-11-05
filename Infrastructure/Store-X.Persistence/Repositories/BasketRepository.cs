using StackExchange.Redis;
using Store_X.Domain.Contracts;
using Store_X.Domain.Entities.Baskets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Store_X.Persistence.Repositories
{
    public class BasketRepository(IConnectionMultiplexer connection) : IBasketRepository
    {

        private readonly IDatabase _database = connection.GetDatabase();

        public async Task<CustomerBasket> GetBasketAsync(string id)
        {
            var redisValue = await _database.StringGetAsync(id);

            if (redisValue.IsNullOrEmpty) return null;
            var basket = JsonSerializer.Deserialize<CustomerBasket>(redisValue);

            if (basket is null) return null;

            return basket;
        }

        public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket, TimeSpan? timeToLive = null)
        {
            var redisValue = JsonSerializer.Serialize<CustomerBasket>(basket);
            var flag = await _database.StringSetAsync(basket.Id, redisValue, TimeSpan.FromDays(30));
            return flag ? await GetBasketAsync(basket.Id) : null!;
        }

        public async Task<bool> DeleteBasketAsync(string id)
        {
            return await _database.KeyDeleteAsync(id);
        }
    }
}
