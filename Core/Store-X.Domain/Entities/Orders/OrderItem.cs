using Store_X.Domain.Entities.Products;

namespace Store_X.Domain.Entities.Orders
{
    public class OrderItem : BaseEntity<int>
    {
        public OrderItem()
        {

        }
        public OrderItem(ProductInOrderItem product, decimal price, int quantity)
        {
            Product = product;
            Price = price;
            Quantity = quantity;
        }

        public ProductInOrderItem Product { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

    }
}