using AutoMapper;

using SLMS.BLL.Interfaces;
using SLMS.DAL.Repositories.Interfaces;
using SLMS.DOL.Entities;
using SLMS.Shared.DTOs.RolePermission;

namespace SLMS.BLL.Services;

public class RolePermissionService
    : IRolePermissionService
{
    private readonly IRolePermissionRepository _repository;

    private readonly IMapper _mapper;

    public RolePermissionService(
        IRolePermissionRepository repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<RolePermissionResponseDto>>
        GetAllAsync()
    {
        var rolePermissions =
            await _repository.GetAllAsync();

        return _mapper.Map<
            IEnumerable<RolePermissionResponseDto>>
            (rolePermissions);
    }

    public async Task<RolePermissionResponseDto>
        CreateAsync(
            RolePermissionCreateDto dto)
    {
        var rolePermission =
            _mapper.Map<RolePermission>(dto);

        await _repository.AddAsync(rolePermission);

        await _repository.SaveChangesAsync();

        return _mapper.Map<
            RolePermissionResponseDto>
            (rolePermission);
    }
}