using SLMS.DOL.Entities;

namespace SLMS.DAL.Repositories.Interfaces;

public interface IInventoryRepository
    : IRepository<InventoryItem>
{

    Task<bool> AccessionNumberExistsAsync(string accessionNumber);

    Task<bool> InventoryNumberExistsAsync(string inventoryNumber);

    Task<bool> ResourceExistsAsync(int resourceId);

}
