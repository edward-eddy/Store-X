using Store_X.Domain.Entities.Products;
using Store_X.Shared.Dtos.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Store_X.Services.Specificatios.Products
{
    public class ProductsWithBrandAndTypeSpecifications : BaseSpecifications<int, Product>
    {
        public ProductsWithBrandAndTypeSpecifications(int id) : base(P => P.Id == id)
        {
            ApplyIncludes();
        }
        public ProductsWithBrandAndTypeSpecifications(ProductQueryParameters parameters) : base
            (
                P =>
                (parameters.BrandId == null || P.BrandId == parameters.BrandId)
                &&
                (parameters.TypeId == null || P.TypeId == parameters.TypeId)
                &&
                (string.IsNullOrEmpty(parameters.Search) || P.Name.ToLower().Contains(parameters.Search.ToLower()))
            )
        {
            // priceasc
            // pricedesc
            // nameasc
            if (!string.IsNullOrEmpty(parameters.Sorting))
            {
                // Check Value
                switch (parameters.Sorting.ToLower())
                {
                    case "priceasc":
                        //OrderBy = P => P.Price;
                        AddOrderBy(P => P.Price);
                        break;
                    case "pricedesc":
                        //OrderByDescending = P =>P.Price;
                        AddOrderByDescending(P => P.Price);
                        break;
                    default:
                        //OrderBy = P => P.Name;
                        AddOrderBy(P => P.Name);
                        break;
                }
            }
            else
            {
                //OrderBy = P => P.Name;
                AddOrderBy(P => P.Name);
                ApplyIncludes();
            }

            Skip = parameters.PageSize * (parameters.PageIndex - 1);
            Take = parameters.PageSize;

        }

        private void ApplyIncludes()
        {
            Includes.Add(P => P.Brand);
            Includes.Add(P => P.Type);
        }
    }
}
