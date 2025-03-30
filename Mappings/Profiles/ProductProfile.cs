using AutoMapper;
using SmartInventoryBE.Models;
using SmartInventoryBE.ProjectAggregate.Request;
using SmartInventoryBE.ProjectAggregate.Response;

namespace SmartInventoryBE.Mappings.Profiles;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        // Product -> ProductResponse
        CreateMap<Product, ProductResponse>()
            .ForMember(dest => dest.Stock, opt => opt.MapFrom(src => src.ProductStock))
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category));
        
        // CreateProductRequest -> Product
        CreateMap<CreateProductRequest, Product>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.ProductName))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.ProductDescription))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.ProductPrice))
            .ForMember(dest => dest.ProductStock, opt => opt.MapFrom(src => src.ProductStock))
            .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.ProductImage))
            .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
            .ForMember(dest => dest.Category, opt => opt.Ignore());
        
        // UpdateProductRequest -> Product
        CreateMap<UpdateProductRequest, Product>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.ProductName))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.ProductDescription))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.ProductPrice))
            .ForMember(dest => dest.ProductStock, opt => opt.MapFrom(src => src.ProductStock))
            .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.ProductImage))
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.Now))
            .ForMember(dest => dest.Category, opt => opt.Ignore())
            .ForMember(dest => dest.CategoryId, opt => opt.Ignore());
    }
} 