using SLMS.Shared.DTOs.LibraryResource;

namespace SLMS.BLL.Interfaces;

public interface ILibraryResourceService
{
    Task<IEnumerable<LibraryResourceResponseDto>>
        GetAllAsync();

    Task<LibraryResourceResponseDto?>
        GetByIdAsync(int id);

    Task<LibraryResourceResponseDto>
        CreateAsync(
            LibraryResourceCreateDto dto);

    Task<LibraryResourceResponseDto?>
        UpdateAsync(
            int id,
            LibraryResourceUpdateDto dto);

    Task<bool>
        DeleteAsync(int id);

    Task<IEnumerable<LibraryResourceResponseDto>>
        SearchAsync(string keyword);
}