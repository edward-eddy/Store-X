using Store_X.Shared.Dtos.Baskets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store_X.Services_Abstractions.Baskets
{
    public interface IBasketService
    {
        Task<BasketDto?> GetBasketAsync(string id);
        Task<BasketDto?> UpdateBasketAsync(BasketDto basketDto);
        Task<bool> DeleteBasketAsync(string id);
    }
}
