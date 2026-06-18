using SLMS.DAL.Data;
using SLMS.DAL.Repositories.Interfaces;
using SLMS.DOL.Entities;

namespace SLMS.DAL.Repositories.Implementations;

public class AuditLogRepository
    : Repository<AuditLog>,
      IAuditLogRepository
{
    public AuditLogRepository(
        SLMSDbContext context)
        : base(context)
    {
    }
}