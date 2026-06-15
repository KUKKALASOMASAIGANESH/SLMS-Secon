using SLMS.Shared.DTOs.Permission;

namespace SLMS.BLL.Interfaces;

public interface IPermissionService
{
    Task<IEnumerable<PermissionResponseDto>>
        GetAllAsync();

    Task<PermissionResponseDto?>
        GetByIdAsync(int id);

    Task<PermissionResponseDto>
        CreateAsync(PermissionCreateDto dto);

    Task<PermissionResponseDto?>
        UpdateAsync(
            int id,
            PermissionUpdateDto dto);

    Task<bool>
        DeleteAsync(int id);
}