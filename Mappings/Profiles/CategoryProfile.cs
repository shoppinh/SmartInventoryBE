using AutoMapper;
using SmartInventoryBE.Models;
using SmartInventoryBE.ProjectAggregate.Request;
using SmartInventoryBE.ProjectAggregate.Response;

namespace SmartInventoryBE.Mappings.Profiles;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        // Category -> CategoryResponse
        CreateMap<Category, CategoryResponse>();
        
        // CreateCategoryRequest -> Category
        CreateMap<CreateCategoryRequest, Category>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.CategoryName));
        
        // UpdateCategoryRequest -> Category
        CreateMap<UpdateCategoryRequest, Category>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.CategoryName))
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.Now));
    }
} 