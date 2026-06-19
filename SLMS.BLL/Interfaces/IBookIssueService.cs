using SLMS.Shared.DTOs.BookIssue;

namespace SLMS.BLL.Interfaces;

public interface IBookIssueService
{
    Task<IEnumerable<BookIssueResponseDto>>
        GetAllAsync();

    Task<BookIssueResponseDto?>
        GetByIdAsync(int id);

    Task<IEnumerable<BookIssueResponseDto>>
    GetOverdueBooksAsync();

    Task<BookIssueResponseDto>
        CreateAsync(BookIssueCreateDto dto);

    Task<BookIssueResponseDto?>
        UpdateAsync(
            int id,
            BookIssueUpdateDto dto);

    Task<bool>
        DeleteAsync(int id);
}