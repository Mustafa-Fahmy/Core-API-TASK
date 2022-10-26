using AutoMapper;
using DomainLayer.Models;
using ServiceLayer.Models;

namespace ServiceLayer.Profiles
{
    public class ProductsProfile : Profile
    {
        public ProductsProfile()
        {
            CreateMap<Products, ProductsForCreationsDto>();
            CreateMap<ProductsForCreationsDto, Products>();
            CreateMap<Products, ProductsDto>();
            CreateMap<ProductsForUpdateDto, Products>();
            CreateMap<Products, ProductsForUpdateDto>();

        }
    }
}
