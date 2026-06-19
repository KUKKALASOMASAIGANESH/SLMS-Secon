using SLMS.Shared.DTOs.BookReturn;

namespace SLMS.BLL.Interfaces;


public interface IBookReturnService
{
    Task<IEnumerable<BookReturnResponseDto>>
        GetAllAsync();

    Task<BookReturnResponseDto?>
        GetByIdAsync(int id);

    Task<BookReturnResponseDto>
        CreateAsync(BookReturnCreateDto dto);

    Task<BookReturnResponseDto?>
        UpdateAsync(
            int id,
            BookReturnUpdateDto dto);

    Task<bool>
        DeleteAsync(int id);
}