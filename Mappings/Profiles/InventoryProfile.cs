using AutoMapper;
using SmartInventoryBE.Models;
using SmartInventoryBE.ProjectAggregate.Request;
using SmartInventoryBE.ProjectAggregate.Response;
using System.Globalization;

namespace SmartInventoryBE.Mappings.Profiles;

public class InventoryProfile : Profile
{
    public InventoryProfile()
    {
        // Inventory -> InventoryResponse
        CreateMap<Inventory, InventoryResponse>()
            .ForMember(dest => dest.Product, opt => opt.MapFrom(src => src.Product));
        
        // CreateInventoryRequest -> Inventory
        CreateMap<CreateInventoryRequest, Inventory>()
            .ForMember(dest => dest.RestockDate, opt => opt.MapFrom(src => 
                DateTime.ParseExact(src.RestockDate, "yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture)))
            .ForMember(dest => dest.Product, opt => opt.Ignore())
            .ForMember(dest => dest.ProductId, opt => opt.Ignore());
        
        // UpdateInventoryRequest -> Inventory
        CreateMap<UpdateInventoryRequest, Inventory>()
            .ForMember(dest => dest.Quantity, opt => opt.Condition(src => src.Quantity.HasValue))
            .ForMember(dest => dest.RestockDate, opt => opt.Condition(src => src.RestockDate.HasValue))
            .ForMember(dest => dest.Product, opt => opt.Ignore());
    }
} 