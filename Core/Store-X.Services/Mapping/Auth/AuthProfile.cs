using AutoMapper;
using Store_X.Domain.Entities.Identity;
using Store_X.Shared.Dtos.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store_X.Services.Mapping.Auth
{
    class AuthProfile : Profile
    {
        public AuthProfile()
        {
            CreateMap<AddressDto, Address>().ReverseMap();
        }
    }
}
