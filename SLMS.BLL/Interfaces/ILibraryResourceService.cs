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
}