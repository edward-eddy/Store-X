

namespace Store_X.Domain.Entities.Products
{
    public class Product : BaseEntity<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }

        public int BrandId { get; set; }
        public ProductBrand Brand { get; set; }

        public int TypeId { get; set; }
        public Product Type { get; set; }
    }
}
