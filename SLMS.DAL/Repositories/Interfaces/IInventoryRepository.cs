using SLMS.DOL.Entities;
using SLMS.Shared.DTOs.Inventory;

namespace SLMS.DAL.Repositories.Interfaces;

public interface IInventoryRepository
    : IRepository<InventoryItem>
{

    Task<bool> AccessionNumberExistsAsync(string accessionNumber);

    Task<bool> InventoryNumberExistsAsync(string inventoryNumber);

    Task<bool> ResourceExistsAsync(int resourceId);

    Task<IEnumerable<InventoryItem>> SearchAsync(
    string? accessionNumber,
    string? inventoryNumber,
    string? shelfNumber,
    int? resourceId,
    string? title,
    string? author,
    string? publisher,
    decimal? minPrice,
    decimal? maxPrice);

    Task<IEnumerable<InventoryItem>> GetByShelfAsync(
    string shelfNumber);

    Task<IEnumerable<ShelfSummaryDto>> GetShelfSummaryAsync();


    Task<InventorySummaryDto>
    GetInventorySummaryAsync();

    Task<IEnumerable<ResourceInventoryReportDto>>
    GetResourceInventoryReportAsync();

    Task<InventoryCostReportDto>
    GetInventoryCostReportAsync();

}
