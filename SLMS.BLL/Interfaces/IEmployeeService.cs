using SLMS.DOL.Entities;
using SLMS.Shared.DTOs.Employee;

namespace SLMS.BLL.Interfaces;

public interface IEmployeeService
{
    Task<IEnumerable<EmployeeResponseDto>>
        GetAllAsync();

    Task<EmployeeResponseDto?>
        GetByIdAsync(int id);

    Task<EmployeeResponseDto>
        CreateAsync(
            EmployeeCreateDto dto);

    Task<EmployeeResponseDto?>
        UpdateAsync(
            int id,
            EmployeeUpdateDto dto);

    Task<bool>
        DeleteAsync(int id);
    Task<IEnumerable<Employee>>
    SearchByNameAsync(string name);
}