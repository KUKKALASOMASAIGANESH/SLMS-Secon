using SLMS.DOL.Entities;

namespace SLMS.BLL.Interfaces;

public interface IDownloadHistoryService
{
    Task<IEnumerable<DownloadHistory>> GetAllAsync();

    Task<DownloadHistory?> GetByIdAsync(int id);

    Task AddAsync(DownloadHistory downloadHistory);

    Task<IEnumerable<DownloadHistory>>
    GetAllWithContentAsync();  
}