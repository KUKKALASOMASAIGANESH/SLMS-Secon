using SLMS.BLL.Interfaces;
using SLMS.DAL.Repositories.Interfaces;
using SLMS.DOL.Entities;

namespace SLMS.BLL.Services;

public class PolicyService : IPolicyService
{
    private readonly IPolicyRepository _repository;

    public PolicyService(
        IPolicyRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Policy>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Policy?> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task AddAsync(Policy policy)
    {
        await _repository.AddAsync(policy);

        await _repository.SaveChangesAsync();
    }

    public async Task UpdateAsync(Policy policy)
    {
        _repository.Update(policy);

        await _repository.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var policy =
            await _repository.GetByIdAsync(id);

        if (policy != null)
        {
            _repository.Delete(policy);

            await _repository.SaveChangesAsync();
        }
    }
}