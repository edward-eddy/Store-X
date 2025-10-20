using Microsoft.EntityFrameworkCore;
using Store_X.Domain.Contracts;
using Store_X.Domain.Entities.Products;
using Store_X.Persistence.Data.Contexts;
using System.Text.Json;

namespace Store_X.Persistence
{
    public class DbInitializer(StoreDbContext _context) : IDbInitializer
    {
        public async Task InitializeAsync()
        {
            // Create Db
            // Update Db
            if (_context.Database.GetPendingMigrationsAsync().GetAwaiter().GetResult().Any())
            {
                await _context.Database.MigrateAsync();

            }

            // Data Seeding

            // ProductBrand
            if (!_context.ProductBrands.Any())
            {
                // 1. Read All Data From Json File 'brands.json'
                // Infrastructure\Store-X.Persistence\Data\DataSeeding\brands.json

                var brandData = await File.ReadAllTextAsync(@"..\Infrastructure\Store-X.Persistence\Data\DataSeeding\brands.json");

                // 2. Convert The JsonString To List<ProductBrand>
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandData);

                if (brands is not null && brands.Count > 0)
                {
                    await _context.ProductBrands.AddRangeAsync(brands);
                }
            }

            // ProductType
            if (!_context.ProductTypes.Any())
            {
                // 1. Read All Data From Json File 
                var typesData = await File.ReadAllTextAsync(@"..\Infrastructure\Store-X.Persistence\Data\DataSeeding\types.json");

                // 2. Convert The JsonString To List<ProductType>
                var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);

                if (types is not null && types.Count > 0)
                {
                    await _context.ProductTypes.AddRangeAsync(types);
                }
            }

            // Product
            if (!_context.Products.Any())
            {
                // 1. Read All Data From Json File 
                var productData = await File.ReadAllTextAsync(@"..\Infrastructure\Store-X.Persistence\Data\DataSeeding\products.json");

                // 2. Convert The JsonString To List<Product>
                var products = JsonSerializer.Deserialize<List<Product>>(productData);

                if (products is not null && products.Count > 0)
                {
                    await _context.Products.AddRangeAsync(products);
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}
