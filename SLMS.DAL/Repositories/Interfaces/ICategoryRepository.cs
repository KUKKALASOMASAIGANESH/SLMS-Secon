using SLMS.DOL.Entities;

namespace SLMS.DAL.Repositories.Interfaces;

public interface ICategoryRepository
    : IRepository<Category>
{
    Task<bool> HasResourcesAsync(int categoryId);
}