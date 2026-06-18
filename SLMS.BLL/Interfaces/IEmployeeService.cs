using SLMS.DOL.Entities;

namespace SLMS.BLL.Interfaces;

public interface IEmployeeService
{
    Task<IEnumerable<Employee>> GetAllAsync();

    Task<Employee?> GetByIdAsync(int id);

    Task AddAsync(Employee employee);

    Task<IEnumerable<Employee>>
    SearchByNameAsync(string name);

    Task UpdateAsync(Employee employee);

    Task DeleteAsync(int id);
}