using SLMS.DOL.Entities;

namespace SLMS.BLL.Interfaces;

public interface ICustodyHistoryService
{
    Task<IEnumerable<CustodyHistory>> GetAllAsync();

    Task<CustodyHistory?> GetByIdAsync(int id);

    Task AddAsync(CustodyHistory custodyHistory);
    
  Task<IEnumerable<CustodyHistory>>
    GetByInventoryItemAsync(int inventoryItemId);
    Task<CustodyHistory?>
    GetCurrentCustodianAsync(int inventoryItemId);
}