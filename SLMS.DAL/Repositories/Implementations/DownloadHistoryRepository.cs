using SLMS.DAL.Data;
using SLMS.DAL.Repositories.Interfaces;
using SLMS.DOL.Entities;

namespace SLMS.DAL.Repositories.Implementations;

public class DownloadHistoryRepository
    : Repository<DownloadHistory>,
      IDownloadHistoryRepository
{
    public DownloadHistoryRepository(
        SLMSDbContext context)
        : base(context)
    {
    }
}