using AutoMapper;
using Ecommerce_API.Models;
using Ecommerce_API.Models.DTOs;
using Ecommerce_API.Models.DTOs.ProductDTOs;

namespace Ecommerce_API
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            // Villa Map

            CreateMap<Product, ProductCreateDTO>();
            CreateMap<ProductCreateDTO, Product>();

            //CreateMap<Villa, VillaCreateDTO>().ReverseMap();
            //CreateMap<Villa, VillaUpdateDTO>().ReverseMap();

            // VillaNumber Map

           // CreateMap<VillaNumber, VillaNumberDTO>();
            //CreateMap<VillaNumberDTO, VillaNumber>();

           // CreateMap<VillaNumber, VillaNumberCreateDTO>().ReverseMap();
           // CreateMap<VillaNumber, VillaNumberUpdateDTO>().ReverseMap();
        }
    }
}
