using SLMS.WebApp.Models.Inventory;

namespace SLMS.WebApp.Services.Interfaces;

public interface IInventoryService
{
    Task<List<InventoryViewModel>>
        GetAllAsync();

    Task<InventoryViewModel?>
        GetByIdAsync(int id);

    Task CreateAsync(
        CreateInventoryViewModel model);

    Task UpdateAsync(
        int id,
        UpdateInventoryViewModel model);

    Task DeleteAsync(int id);

    Task<List<InventoryViewModel>>
    SearchAsync(string searchTerm);

    Task<InventoryPagedViewModel>
      GetPagedAsync(
          int page,
          int pageSize);

    Task<InventorySummaryViewModel>
    GetSummaryAsync();

    Task<List<ResourceInventoryReportViewModel>>
    GetResourceReportAsync();

    Task<InventoryCostReportViewModel>
    GetCostReportAsync();

    Task<InventoryAvailabilityReportViewModel>
    GetAvailabilityReportAsync();

    Task<List<ShelfReportViewModel>>
    GetShelfReportAsync();

    Task<List<InventoryViewModel>>
    GetInactiveAsync();

    Task RestoreAsync(int id);

    Task<List<InventoryViewModel>>
    GetSortedAsync(
        string sortBy,
        bool descending);
}