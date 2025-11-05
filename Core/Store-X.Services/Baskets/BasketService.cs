using AutoMapper;
using Store_X.Domain.Contracts;
using Store_X.Domain.Entities.Baskets;
using Store_X.Domain.Exceptions;
using Store_X.Services_Abstractions.Baskets;
using Store_X.Shared.Dtos.Baskets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store_X.Services.Baskets
{
    public class BasketService(IBasketRepository basketRepository, IMapper _mapper) : IBasketService
    {
        public async Task<BasketDto?> GetBasketAsync(string id)
        {
            var basket = await basketRepository.GetBasketAsync(id);
            if (basket == null) throw new BasketNotFoundException(id);
            var result = _mapper.Map<BasketDto>(basket);
            return result;
        }

        public async Task<BasketDto?> UpdateBasketAsync(BasketDto basketDto)
        {
            var basket = _mapper.Map<CustomerBasket>(basketDto);
            basket = await basketRepository.UpdateBasketAsync(basket);
            if (basket is null) throw new BasketCreateOrUpdateBadRequestException();
            return _mapper.Map<BasketDto>(basket);
        }

        public async Task<bool> DeleteBasketAsync(string id)
        {
            var flag = await basketRepository.DeleteBasketAsync(id);
            return flag ? flag : throw new BasketDeleteBadRequestException();
        }
    }
}
