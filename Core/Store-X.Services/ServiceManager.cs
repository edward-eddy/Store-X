using AutoMapper;
using Store_X.Domain.Contracts;
using Store_X.Services.Products;
using Store_X.Services_Abstractions;
using Store_X.Services_Abstractions.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store_X.Services
{
    public class ServiceManager(IUnitOfWork _unitOfWork, IMapper _mapper) : IServiceManager
    {
        public IProductService ProductService { get; } = new ProductService(_unitOfWork, _mapper);
    }
}
