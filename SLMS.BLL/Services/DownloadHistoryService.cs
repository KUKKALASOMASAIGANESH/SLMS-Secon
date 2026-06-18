using SLMS.BLL.Interfaces;
using SLMS.DAL.Repositories.Interfaces;
using SLMS.DOL.Entities;

namespace SLMS.BLL.Services;

public class DownloadHistoryService
    : IDownloadHistoryService
{
    private readonly IDownloadHistoryRepository _repository;

    public DownloadHistoryService(
        IDownloadHistoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<DownloadHistory>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<DownloadHistory?> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task AddAsync(
        DownloadHistory downloadHistory)
    {
        await _repository.AddAsync(downloadHistory);

        await _repository.SaveChangesAsync();
    }

    public async Task<IEnumerable<DownloadHistory>>
    GetAllWithContentAsync()
    {
        return await _repository.GetAllWithContentAsync();
    }
}