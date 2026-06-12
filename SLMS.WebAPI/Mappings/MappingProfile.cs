using AutoMapper;
using SLMS.DOL.Entities;
using SLMS.Shared.DTOs.Inventory;

namespace SLMS.WebAPI.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<InventoryItem, InventoryItemDto>();

        CreateMap<CreateInventoryItemDto, InventoryItem>();

        CreateMap<UpdateInventoryItemDto, InventoryItem>();
    }
}