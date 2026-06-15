using SLMS.BLL.Interfaces;
using SLMS.DAL.Repositories.Interfaces;
using SLMS.DOL.Entities;

namespace SLMS.BLL.Services;

public class AuditLogService : IAuditLogService
{
    private readonly IAuditLogRepository _repository;

    public AuditLogService(
        IAuditLogRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<AuditLog>>
        GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<AuditLog?>
        GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task AddAsync(
        AuditLog auditLog)
    {
        await _repository.AddAsync(auditLog);

        await _repository.SaveChangesAsync();
    }
}