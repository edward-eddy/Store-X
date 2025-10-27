using Store_X.Domain.Entities.Products;
using Store_X.Shared.Dtos.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store_X.Services.Specificatios.Products
{
    public class ProductsCountSpecifications : BaseSpecifications<int, Product>
    {
        public ProductsCountSpecifications(ProductQueryParameters parameters) : base
            (
                P =>
                (parameters.BrandId == null || P.BrandId == parameters.BrandId)
                &&
                (parameters.TypeId == null || P.TypeId == parameters.TypeId)
                &&
                (string.IsNullOrEmpty(parameters.Search) || P.Name.ToLower().Contains(parameters.Search.ToLower()))
            )
        {

        }
    }
}
