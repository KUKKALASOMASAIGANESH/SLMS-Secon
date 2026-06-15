using SLMS.Shared.DTOs.RolePermission;

namespace SLMS.BLL.Interfaces;

public interface IRolePermissionService
{
    Task<IEnumerable<RolePermissionResponseDto>>
        GetAllAsync();

    Task<RolePermissionResponseDto>
        CreateAsync(RolePermissionCreateDto dto);
}