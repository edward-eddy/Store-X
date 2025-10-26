using Store_X.Shared.Dtos.Products;

namespace Store_X.Services_Abstractions.Products
{
    public interface IProductService
    {
        Task<IEnumerable<ProductResponce>> GetAllProductsAsync(int? brandId, int? typeId);
        Task<ProductResponce> GetProductByIdAsync(int id);
        Task<IEnumerable<BrandTypeResponse>> GetAllBrandsAsync();
        Task<IEnumerable<BrandTypeResponse>> GetAllTypesAsync();
    }
}
