using SLMS.DAL.Data;
using SLMS.DAL.Repositories.Interfaces;
using SLMS.DOL.Entities;

namespace SLMS.DAL.Repositories.Implementations;

public class PermissionRepository
    : Repository<Permission>,
      IPermissionRepository
{
    public PermissionRepository(
        SLMSDbContext context)
        : base(context)
    {
    }
}