using SLMS.Shared.DTOs.Shelf;

namespace SLMS.BLL.Interfaces;

public interface IShelfService
{
    Task<IEnumerable<ShelfResponseDto>>
        GetAllAsync();

    Task<ShelfResponseDto?>
        GetByIdAsync(int id);

    Task<ShelfResponseDto>
        CreateAsync(
            ShelfCreateDto dto);

    Task<ShelfResponseDto?>
        UpdateAsync(
            int id,
            ShelfUpdateDto dto);

    Task<bool>
        DeleteAsync(int id);
}
