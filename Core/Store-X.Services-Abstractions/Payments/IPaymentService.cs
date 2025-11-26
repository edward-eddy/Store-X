using Store_X.Shared.Dtos.Baskets;

namespace Store_X.Services_Abstractions.Payments
{
    public interface IPaymentService
    {
        Task<BasketDto> CreatePaymentIntentAsync(string basketId);
    }
}
