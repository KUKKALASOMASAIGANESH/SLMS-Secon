using SLMS.BLL.Interfaces;
using SLMS.DAL.Repositories.Interfaces;
using SLMS.DOL.Entities;

namespace SLMS.BLL.Services;

public class DigitalContentService
    : IDigitalContentService
{
    private readonly IDigitalContentRepository _repository;

    public DigitalContentService(
        IDigitalContentRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<DigitalContent>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<DigitalContent?> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task AddAsync(
        DigitalContent digitalContent)
    {
        await _repository.AddAsync(digitalContent);

        await _repository.SaveChangesAsync();
    }

    public async Task UpdateAsync(
        DigitalContent digitalContent)
    {
        _repository.Update(digitalContent);

        await _repository.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var content =
            await _repository.GetByIdAsync(id);

        if (content == null)
            return;

        _repository.Delete(content);

        await _repository.SaveChangesAsync();
    }
}