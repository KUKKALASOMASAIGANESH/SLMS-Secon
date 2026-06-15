using SLMS.DAL.Data;
using SLMS.DAL.Repositories.Interfaces;
using SLMS.DOL.Entities;

namespace SLMS.DAL.Repositories.Implementations;

public class RoleRepository
    : Repository<Role>,
      IRoleRepository
{
    public RoleRepository(
        SLMSDbContext context)
        : base(context)
    {
    }
}