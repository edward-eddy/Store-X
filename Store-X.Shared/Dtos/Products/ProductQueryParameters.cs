using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store_X.Shared.Dtos.Products
{
    public class ProductQueryParameters
    {
        //int? brandId, int? typeId, string? sort, string? search, int pageSize = 5, int pageIndex = 1
        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
        public string? Sorting { get; set; }
        public string? Search { get; set; }
        public int PageSize { get; set; } = 5;
        public int PageIndex { get; set; } = 1;
    }
}
