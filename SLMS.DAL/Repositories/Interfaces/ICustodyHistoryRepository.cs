using SLMS.DOL.Entities;
using SLMS.Shared.DTOs.Custody;

namespace SLMS.DAL.Repositories.Interfaces;

public interface ICustodyHistoryRepository
    : IRepository<CustodyHistory>
{
    Task<IEnumerable<CustodyHistory>>
    GetAllWithDetailsAsync();

    Task<IEnumerable<CustodyHistory>>
    GetByInventoryItemAsync(int inventoryItemId);

    Task<CustodyHistory?>
    GetCurrentCustodianAsync(int inventoryItemId);
    Task<IEnumerable<CustodyHistoryReportDto>>
       GetCustodyReportAsync();

}