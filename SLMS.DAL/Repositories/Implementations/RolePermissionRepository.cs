using SLMS.DAL.Data;
using SLMS.DAL.Repositories.Interfaces;
using SLMS.DOL.Entities;

namespace SLMS.DAL.Repositories.Implementations;

public class RolePermissionRepository
    : Repository<RolePermission>,
      IRolePermissionRepository
{
    public RolePermissionRepository(
        SLMSDbContext context)
        : base(context)
    {
    }
}