using SLMS.Shared.DTOs.Department;

namespace SLMS.BLL.Interfaces;

public interface IDepartmentService
{
    Task<IEnumerable<DepartmentResponseDto>> GetAllAsync();

    Task<DepartmentResponseDto?> GetByIdAsync(int id);

    Task<DepartmentResponseDto> CreateAsync(
        DepartmentCreateDto dto);
}