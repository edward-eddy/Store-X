using AutoMapper;
using Microsoft.Extensions.Configuration;
using Store_X.Domain.Entities.Products;
using Store_X.Shared.Dtos.Products;

namespace Store_X.Services.Mapping.Products
{
    public class ProductProfile : Profile
    {
        public ProductProfile(IConfiguration configuration)
        {
            CreateMap<Product, ProductResponce>()
                .ForMember(D => D.Brand, O => O.MapFrom(S => S.Brand.Name))
                .ForMember(D => D.Type, O => O.MapFrom(S => S.Type.Name))
                //.ForMember(D => D.PictureUrl, O => O.MapFrom(S => $"{configuration["BaseUre"]}/{S.PictureUrl}"))
                .ForMember(D => D.PictureUrl, O => O.MapFrom(new ProductPictureUrlResolver(configuration)))
                ;

            CreateMap<ProductBrand, BrandTypeResponse>();
            CreateMap<ProductType, BrandTypeResponse>();
        }
    }
}
