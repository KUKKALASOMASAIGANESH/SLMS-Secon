using SLMS.Shared.DTOs.Role;

namespace SLMS.BLL.Interfaces;

public interface IRoleService
{
    Task<IEnumerable<RoleResponseDto>>
        GetAllAsync();

    Task<RoleResponseDto?>
        GetByIdAsync(int id);

    Task<RoleResponseDto>
        CreateAsync(RoleCreateDto dto);

    Task<RoleResponseDto?>
        UpdateAsync(
            int id,
            RoleUpdateDto dto);

    Task<bool>
        DeleteAsync(int id);
}