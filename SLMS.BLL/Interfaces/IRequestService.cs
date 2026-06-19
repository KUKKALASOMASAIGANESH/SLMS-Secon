using SLMS.Shared.DTOs.Request;

namespace SLMS.BLL.Interfaces;

public interface IRequestService
{
    Task<IEnumerable<RequestResponseDto>> GetAllAsync();

    Task<RequestResponseDto?> GetByIdAsync(int id);

    Task<RequestResponseDto> CreateAsync(RequestCreateDto dto);

    Task<RequestResponseDto?> UpdateAsync(
        int id,
        RequestUpdateDto dto);

    Task<bool> DeleteAsync(int id);

    Task<RequestResponseDto?> ApproveAsync(int id);

    Task<RequestResponseDto?> RejectAsync(int id);
}