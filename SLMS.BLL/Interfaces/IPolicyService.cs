using SLMS.DOL.Entities;

namespace SLMS.BLL.Interfaces;

public interface IPolicyService
{
    Task<IEnumerable<Policy>> GetAllAsync();

    Task<Policy?> GetByIdAsync(int id);

    Task AddAsync(Policy policy);

    Task UpdateAsync(Policy policy);

    Task DeleteAsync(int id);
}   