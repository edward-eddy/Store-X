using Microsoft.AspNetCore.Mvc;
using Store_X.Services_Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store_X.Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController(IServiceManager _serviceManager) : ControllerBase
    {
        [HttpGet] // GET: baseUrl/api/products
        public async Task<IActionResult> GetAllProducts(int? brandId, int? typeId, string? sort, string? search)
        {
            var result = await _serviceManager.ProductService.GetAllProductsAsync(brandId, typeId, sort, search);
            if (result is null) return BadRequest();
            return Ok(result);
        }

        [HttpGet("{id}")] // GET: baseUrl/api/products/{id}
        public async Task<IActionResult> GetProductById(int? id)
        {
            if (id is null) return BadRequest();

            var result = await _serviceManager.ProductService.GetProductByIdAsync(id.Value);

            if (result is null) return NotFound();

            return Ok(result);
        }

        [HttpGet("Brands")] // GET: baseUrl/api/products/brands
        public async Task<IActionResult> GetAllBrands()
        {
            var result = await _serviceManager.ProductService.GetAllBrandsAsync();
            if (result is null) return BadRequest();
            return Ok(result);
        }

        [HttpGet("Types")] // GET: baseUrl/api/products/types
        public async Task<IActionResult> GetAllTypes()
        {
            var result = await _serviceManager.ProductService.GetAllTypesAsync();
            if (result is null) return BadRequest();
            return Ok(result);
        }
    }
}
