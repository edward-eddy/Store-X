using AutoMapper;
using Store_X.Domain.Contracts;
using Store_X.Domain.Entities.Orders;
using Store_X.Domain.Entities.Products;
using Store_X.Domain.Exceptions.BadRequest;
using Store_X.Domain.Exceptions.NotFound;
using Store_X.Services_Abstractions.Baskets;
using Store_X.Services_Abstractions.Orders;
using Store_X.Shared.Dtos.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store_X.Services.Orders
{
    //public class OrderService(IUnitOfWork _unitOfWork, IBasketService _basketService, IMapper _mapper) : IOrderService
    public class OrderService(IUnitOfWork _unitOfWork, IBasketRepository _basketRepository, IMapper _mapper) : IOrderService
    {
        public async Task<OrderResponse?> CreateOrderAsync(OrderRequest request, string userEmail)
        {
            // 1. Get Order Address
            var orderAddress = _mapper.Map<OrderAddress>(request.ShipToAddress);

            // 2. Get Delivery Method By Id
            var deliveryMethod = await _unitOfWork.GetRepository<int, DeliveryMethod>().GetAsync(request.DeliveryMethodId);
            if (deliveryMethod is null) throw new DeliveryMethodNotFoundException(request.DeliveryMethodId);

            //3. Get Order Item
            // 3.1. Get Basket By Id
            //var basket = await _basketService.GetBasketAsync(request.BasketId);
            var basket = await _basketRepository.GetBasketAsync(request.BasketId);
            if (basket is null) throw new BasketNotFoundException(request.BasketId);

            // 3.2. Convert Every basketItem To OrderItem
            var orderItems = new List<OrderItem>();
            foreach (var item in basket.Items)
            {
                // Check Price
                // Get Product From Database
                var product = await _unitOfWork.GetRepository<int, Product>().GetAsync(item.Id);
                if (product is null) throw new ProductNotFoundException(item.Id);

                if (product.Price != item.Price) item.Price = product.Price;

                var productInOrderItem = new ProductInOrderItem(item.Id, item.ProductName, item.PictureUrl);
                var orderItem = new OrderItem(productInOrderItem, item.Price, item.Quantity);
                orderItems.Add(orderItem);
            }

            // 4. SubTotal
            var subTotal = basket.Items.Sum(OI => OI.Price * OI.Quantity);


            var order = new Order(userEmail, orderAddress, deliveryMethod, orderItems, subTotal);
            await _unitOfWork.GetRepository<Guid, Order>().AddAsync(order);
            var count = await _unitOfWork.SaveChangesAsync();
            if (count <= 0) throw new OrderCreateBadRequestException();

            return _mapper.Map<OrderResponse>(order);
        }

        public Task<IEnumerable<DeliveryMethodResponse>> GetAllMethodAsync()
        {
            throw new NotImplementedException();
        }

        public Task<OrderResponse?> GetOrderByIdForSpecificUserAsync(Guid id, string userEmail)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<OrderResponse>> GetOrdersForSpecificUserAsync(string userEmail)
        {
            throw new NotImplementedException();
        }
    }
}
