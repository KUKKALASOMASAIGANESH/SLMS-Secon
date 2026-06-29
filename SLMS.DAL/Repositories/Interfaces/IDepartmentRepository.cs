using SLMS.DOL.Entities;

namespace SLMS.DAL.Repositories.Interfaces;

public interface IDepartmentRepository : IRepository<Department>
{
    Task<IEnumerable<Department>>
    SearchAsync(string keyword);
}