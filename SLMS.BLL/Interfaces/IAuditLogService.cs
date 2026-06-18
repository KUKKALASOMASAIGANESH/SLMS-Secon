using SLMS.DOL.Entities;

namespace SLMS.BLL.Interfaces;

public interface IAuditLogService
{
    Task<IEnumerable<AuditLog>> GetAllAsync();

    Task<AuditLog?> GetByIdAsync(int id);

    Task AddAsync(AuditLog auditLog);
}