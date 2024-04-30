using AboHalalBackend.Controllers;
using AboHalalBackend.Dtos.Category;
using AboHalalBackend.Dtos.Product;
using AboHalalBackend.Models;
using AutoMapper;

namespace AboHalalBackend
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Models.Category, GetCategoryDto>()
                    .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products));

            CreateMap<Product, ProductDto>();
        }
    }
}
