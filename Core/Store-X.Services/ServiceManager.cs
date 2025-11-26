using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Store_X.Domain.Contracts;
using Store_X.Domain.Entities.Identity;
using Store_X.Services.Auth;
using Store_X.Services.Baskets;
using Store_X.Services.Cache;
using Store_X.Services.Orders;
using Store_X.Services.Payments;
using Store_X.Services.Products;
using Store_X.Services_Abstractions;
using Store_X.Services_Abstractions.Auth;
using Store_X.Services_Abstractions.Baskets;
using Store_X.Services_Abstractions.Cache;
using Store_X.Services_Abstractions.Orders;
using Store_X.Services_Abstractions.Payments;
using Store_X.Services_Abstractions.Products;
using Store_X.Shared;

namespace Store_X.Services
{
    public class ServiceManager(
        IUnitOfWork _unitOfWork,
        IMapper _mapper,
        IBasketRepository _basketRepository,
        ICacheRepository _cacheRepository,
        //IConfiguration _configuration,
        IOptions<JwtOptions> options,
        UserManager<AppUser> _userManager,
        IConfiguration configuration
        ) : IServiceManager
    {
        public IProductService ProductService { get; } = new ProductService(_unitOfWork, _mapper);
        public IBasketService BasketService { get; } = new BasketService(_basketRepository, _mapper);
        public ICacheService CacheService { get; } = new CacheService(_cacheRepository);
        public IAuthServices AuthServices { get; } = new AuthService(_userManager, options, _mapper);
        public IOrderService OrderService { get; } = new OrderService(_unitOfWork, _basketRepository, _mapper);
        public IPaymentService PaymentService { get; } = new PaymentService(_basketRepository, _unitOfWork, configuration, _mapper);
    }
}
