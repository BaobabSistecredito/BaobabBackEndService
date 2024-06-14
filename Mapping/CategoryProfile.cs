using AutoMapper;
using BaobabBackEndService.DTOs;
using BaobabBackEndSerice.Models;

namespace BaobabBackEndService.Mapping
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryDTO>();
            CreateMap<CategoryDTO, Category>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(e => e.CategoryName.ToLower()));
        }
    }
}
