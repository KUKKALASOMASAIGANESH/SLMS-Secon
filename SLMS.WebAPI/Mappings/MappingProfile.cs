using AutoMapper;

using SLMS.DOL.Entities;

using SLMS.Shared.DTOs.Category;
using SLMS.Shared.DTOs.LibraryResource;

namespace SLMS.WebAPI.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Category

        CreateMap<CategoryCreateDto, Category>();

        CreateMap<Category, CategoryResponseDto>();


        // Library Resource

        CreateMap<LibraryResourceCreateDto, LibraryResource>();

        CreateMap<LibraryResource, LibraryResourceResponseDto>();

        CreateMap<LibraryResourceUpdateDto, LibraryResource>();
    }
}