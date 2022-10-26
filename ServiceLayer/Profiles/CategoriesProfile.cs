using AutoMapper;
using DomainLayer.Models;
using ServiceLayer.Models;

namespace ServiceLayer.Profiles
{
    public class CategoriesProfile : Profile
    {
        public CategoriesProfile()
        {
            CreateMap<Category, CategoryDto>();

        }
    }
}
