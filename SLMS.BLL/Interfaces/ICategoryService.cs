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
}