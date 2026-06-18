using AutoMapper;

using SLMS.DOL.Entities;

using SLMS.Shared.DTOs.Department;
using SLMS.Shared.DTOs.Inventory;

namespace SLMS.WebAPI.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Department
        CreateMap<DepartmentCreateDto, Department>();

        CreateMap<DepartmentUpdateDto, Department>();

        CreateMap<Department, DepartmentResponseDto>();

        // Inventory
        CreateMap<InventoryItem, InventoryItemDto>();

        CreateMap<CreateInventoryItemDto, InventoryItem>();

        CreateMap<UpdateInventoryItemDto, InventoryItem>();
    }
}