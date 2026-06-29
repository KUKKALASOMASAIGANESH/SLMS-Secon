using SLMS.DOL.Entities;

namespace SLMS.DAL.Repositories.Interfaces;

public interface IEmployeeRepository
    : IRepository<Employee>
{
    Task<IEnumerable<Employee>> SearchByNameAsync(string name);
}