using Store_X.Domain.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store_X.Services.Specificatios.Orders
{
    public class OrdersSpecifications : BaseSpecifications<Guid, Order>
    {
        public OrdersSpecifications(Guid id, string userEmail) : base(O => O.Id == id && O.UserEmail.ToLower() == userEmail.ToLower())
        {
            Includes.Add(O => O.DeliveryMethod);
            Includes.Add(O => O.Items);
        }
        public OrdersSpecifications(string userEmail) : base(O => O.UserEmail.ToLower() == userEmail.ToLower())
        {
            Includes.Add(O => O.DeliveryMethod);
            Includes.Add(O => O.Items);

            AddOrderByDescending(O => O.OrderDate);
        }
    }
}
