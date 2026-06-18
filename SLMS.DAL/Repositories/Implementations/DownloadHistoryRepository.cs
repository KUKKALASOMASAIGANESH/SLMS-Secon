using SLMS.DAL.Data;
using SLMS.DAL.Repositories.Interfaces;
using SLMS.DOL.Entities;
using Microsoft.EntityFrameworkCore;

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
    public async Task<IEnumerable<DownloadHistory>>
    GetAllWithContentAsync()
    {
        return await _context.Set<DownloadHistory>()
            .Include(x => x.DigitalContent)
            .ToListAsync();
    }
}