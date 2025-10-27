using Store_X.Domain.Entities.Products;
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
        public ProductsWithBrandAndTypeSpecifications(int? brandId, int? typeId, string? sort) : base
            (
                P =>
                (brandId == null || P.BrandId == brandId)
                &&
                (typeId == null || P.TypeId == typeId)
            )
        {
            // priceasc
            // pricedesc
            // nameasc
            if (!string.IsNullOrEmpty(sort))
            {
                // Check Value
                switch (sort.ToLower())
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
        }

        private void ApplyIncludes()
        {
            Includes.Add(P => P.Brand);
            Includes.Add(P => P.Type);
        }
    }
}
