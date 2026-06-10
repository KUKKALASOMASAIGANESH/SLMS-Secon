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
}