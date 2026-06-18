using SLMS.DOL.Entities;

namespace SLMS.DAL.Repositories.Interfaces;

public interface IDownloadHistoryRepository
    : IRepository<DownloadHistory>
{
    Task<IEnumerable<DownloadHistory>>
    GetAllWithContentAsync();
}