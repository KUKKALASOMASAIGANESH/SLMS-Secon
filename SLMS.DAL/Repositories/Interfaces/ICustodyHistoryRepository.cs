using SLMS.DOL.Entities;

namespace SLMS.DAL.Repositories.Interfaces;

public interface ICustodyHistoryRepository
    : IRepository<CustodyHistory>
{
    Task<IEnumerable<CustodyHistory>>
    GetByInventoryItemAsync(int inventoryItemId);

    Task<CustodyHistory?>
    GetCurrentCustodianAsync(int inventoryItemId);
}