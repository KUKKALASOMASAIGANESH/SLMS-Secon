using SLMS.DOL.Entities;

namespace SLMS.BLL.Interfaces;

public interface IDepartmentService
{
    Task<IEnumerable<Department>> GetAllAsync();

    Task<Department?> GetByIdAsync(int id);

    Task AddAsync(Department department);
}