using SLMS.BLL.Interfaces;
using SLMS.DAL.Repositories.Interfaces;
using SLMS.DOL.Entities;

namespace SLMS.BLL.Services;

public class DigitalContentRequestService
    : IDigitalContentRequestService
{
    private readonly IDigitalContentRequestRepository _repository;

    public DigitalContentRequestService(
        IDigitalContentRequestRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<DigitalContentRequest>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<DigitalContentRequest?> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task AddAsync(
        DigitalContentRequest request)
    {
        await _repository.AddAsync(request);

        await _repository.SaveChangesAsync();
    }
}