using SLMS.DOL.Entities;

namespace SLMS.DAL.Repositories.Interfaces;

public interface IShelfRepository
{
    Task<IEnumerable<Shelf>> GetAllAsync();

    Task<Shelf?> GetByIdAsync(int id);

    Task AddAsync(Shelf shelf);

    void Update(Shelf shelf);

    void Delete(Shelf shelf);

    Task SaveChangesAsync();

    Task<bool> HasResourcesAsync(int shelfId);

    Task<bool> ExistsByNameForUpdateAsync(
    string shelfName,
    int shelfId);

    Task<Shelf?> GetByIdWithResourcesAsync(int id);
}
