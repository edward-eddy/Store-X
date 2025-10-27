using Store_X.Shared.Dtos.Products;

namespace Store_X.Services_Abstractions.Products
{
    public interface IProductService
    {
        Task<IEnumerable<ProductResponce>> GetAllProductsAsync(ProductQueryParameters parameters);
        Task<ProductResponce> GetProductByIdAsync(int id);
        Task<IEnumerable<BrandTypeResponse>> GetAllBrandsAsync();
        Task<IEnumerable<BrandTypeResponse>> GetAllTypesAsync();
    }
}
