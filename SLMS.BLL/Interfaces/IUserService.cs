using SLMS.Shared.DTOs.User;

namespace SLMS.BLL.Interfaces;

public interface IUserService
{
    Task<IEnumerable<UserResponseDto>>
        GetAllAsync();

    Task<UserResponseDto?>
        GetByIdAsync(int id);

    Task<UserResponseDto>
        CreateAsync(UserCreateDto dto);

    Task<UserResponseDto?>
        UpdateAsync(
            int id,
            UserUpdateDto dto);

    Task<bool>
        DeleteAsync(int id);
}