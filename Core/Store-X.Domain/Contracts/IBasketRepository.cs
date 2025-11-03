using Store_X.Domain.Entities.Baskets;

namespace Store_X.Domain.Contracts
{
    public interface IBasketRepository
    {
        Task<CustomerBasket> GetBasketAsync(string id);
        Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket, TimeSpan? timeToLive = null);
        Task<bool> DeleteBasketAsync(string id);
    }
}
