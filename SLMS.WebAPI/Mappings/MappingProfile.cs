using AutoMapper;
using SLMS.DOL.Entities;
using SLMS.Shared.DTOs.Department;

namespace SLMS.WebAPI.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<DepartmentCreateDto, Department>();

        CreateMap<DepartmentUpdateDto, Department>();

        CreateMap<Department, DepartmentResponseDto>();
    }
}