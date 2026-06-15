/*
 * MappingProfile
 *
 * Purpose:
 * Configures AutoMapper mappings between domain entities
 * and Data Transfer Objects (DTOs).
 *
 * Responsibilities:
 * - Entity to DTO mapping
 * - DTO to Entity mapping
 * - Reducing manual object transformation code
 *
 * Architecture Flow:
 * Controller
 *      ↓
 * DTO
 *      ↓
 * AutoMapper (MappingProfile)
 *      ↓
 * Entity
 *      ↓
 * Database
 *
 * This profile centralizes mapping configurations used
 * throughout the application and ensures consistent
 * transformation between entities and DTOs.
 */


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