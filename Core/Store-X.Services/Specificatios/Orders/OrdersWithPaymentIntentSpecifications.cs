using Store_X.Domain.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store_X.Services.Specificatios.Orders
{
    public class OrdersWithPaymentIntentSpecifications : BaseSpecifications<Guid, Order>
    {
        public OrdersWithPaymentIntentSpecifications(string paymentIntentId) : base(O => O.PaymentIntentId == paymentIntentId)
        {
        }
    }
}
