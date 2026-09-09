using Microsoft.EntityFrameworkCore;
using SLMS.DAL.Data;
using SLMS.DAL.Repositories.Interfaces;
using SLMS.DOL.Entities;

namespace SLMS.DAL.Repositories.Implementations;

public class DepartmentRepository
    : Repository<Department>, IDepartmentRepository
{
    public DepartmentRepository(
        SLMSDbContext context)
        : base(context)
    {
    }
    public async Task<IEnumerable<Department>>
    SearchAsync(string keyword)
    {
        return await _dbSet
            .Where(x =>
                x.DepartmentCode.Contains(keyword) ||
                x.DepartmentName.Contains(keyword))
            .ToListAsync();
    }
}