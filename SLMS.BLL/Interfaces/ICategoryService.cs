using SLMS.Shared.DTOs.Category;

namespace SLMS.BLL.Interfaces;

public interface ICategoryService
{
    Task<IEnumerable<CategoryResponseDto>>
        GetAllAsync();

    Task<CategoryResponseDto?>
        GetByIdAsync(int id);

    Task<CategoryResponseDto>
        CreateAsync(
            CategoryCreateDto dto);
    Task<CategoryResponseDto?> UpdateAsync(
    int id,
    CategoryUpdateDto dto);

    Task<bool> DeleteAsync(int id);
}