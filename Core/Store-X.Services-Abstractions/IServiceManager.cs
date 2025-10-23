using Store_X.Services_Abstractions.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store_X.Services_Abstractions
{
    public interface IServiceManager
    {
        public IProductService ProductService { get; }
    }
}
