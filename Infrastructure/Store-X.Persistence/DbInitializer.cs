using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Store_X.Domain.Contracts;
using Store_X.Domain.Entities.Identity;
using Store_X.Domain.Entities.Orders;
using Store_X.Domain.Entities.Products;
using Store_X.Persistence.Data.Contexts;
using Store_X.Persistence.Identity.Contexts;
using System.Text.Json;

namespace Store_X.Persistence
{
    public class DbInitializer(
        StoreDbContext _context,
        IdentityStoreDbContext _storeContext,
        UserManager<AppUser> _userManager,
        RoleManager<IdentityRole> _roleManager
        ) : IDbInitializer
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

            // Delivery Methods
            if (!_context.DeliveryMethods.Any())
            {
                // 1. Read All Data From Json File 'delivery.json'
                // Infrastructure\Store-X.Persistence\Data\DataSeeding\delivery.json

                var deliveryData = await File.ReadAllTextAsync(@"..\Infrastructure\Store-X.Persistence\Data\DataSeeding\delivery.json");

                // 2. Convert The JsonString To List<DeliveryMethods>
                var methods = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliveryData);

                if (methods is not null && methods.Count > 0)
                {
                    await _context.DeliveryMethods.AddRangeAsync(methods);
                }
            }

            await _context.SaveChangesAsync();
        }

        public async Task InitializeIdentityAsync()
        {
            // Create Db
            // Update Db
            if (_storeContext.Database.GetPendingMigrationsAsync().GetAwaiter().GetResult().Any())
            {
                await _storeContext.Database.MigrateAsync();
            }

            // Data Seeding
            if (!_storeContext.Roles.Any())
            {
                await _roleManager.CreateAsync(new IdentityRole() { Name = "SuperAdmin" });
                await _roleManager.CreateAsync(new IdentityRole() { Name = "Admin" });
            }

            if (!_storeContext.Users.Any())
            {
                var superAdmin = new AppUser()
                {
                    UserName = "SuperAdmin",
                    DisplayName = "SuperAdmin",
                    Email = "SuperAdmin@gmail.com",
                    PhoneNumber = "01234567890",
                };

                var admin = new AppUser()
                {
                    UserName = "Admin",
                    DisplayName = "Admin",
                    Email = "Admin@gmail.com",
                    PhoneNumber = "01234567890",
                };

                await _userManager.CreateAsync(superAdmin, "P@ssW0rd");
                await _userManager.CreateAsync(admin, "P@ssW0rd");

                await _userManager.AddToRolesAsync(superAdmin, ["SuperAdmin", "Admin"]);
                await _userManager.AddToRoleAsync(admin, "Admin");
            }
        }
    }
}
