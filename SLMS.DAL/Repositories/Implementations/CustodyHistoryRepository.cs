using SLMS.DAL.Data;
using SLMS.DAL.Repositories.Interfaces;
using SLMS.DOL.Entities;

namespace SLMS.DAL.Repositories.Implementations;

public class CustodyHistoryRepository
    : Repository<CustodyHistory>, ICustodyHistoryRepository
{
    public CustodyHistoryRepository(
        SLMSDbContext context)
        : base(context)
    {
    }
}