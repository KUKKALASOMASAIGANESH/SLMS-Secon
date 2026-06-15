using SLMS.Shared.DTOs.Inventory;

namespace SLMS.BLL.Interfaces;
using SLMS.Shared.DTOs.Common;

public interface IInventoryService
{
    Task<IEnumerable<InventoryItemDto>> GetAllAsync();

    Task<InventoryItemDto?> GetByIdAsync(int id);

    Task<IEnumerable<InventoryItemDto>> SearchAsync(
    InventorySearchDto searchDto);

    Task<IEnumerable<InventoryItemDto>>
    GetByShelfAsync(string shelfNumber);

    Task<IEnumerable<ShelfSummaryDto>>
        GetShelfSummaryAsync();

    Task CreateAsync(CreateInventoryItemDto dto);

    Task<bool> UpdateAsync(
    int id,
    UpdateInventoryItemDto dto);
    Task<bool> DeleteAsync(int id);

    Task<InventorySummaryDto>
    GetInventorySummaryAsync();

    Task<IEnumerable<ResourceInventoryReportDto>>
    GetResourceInventoryReportAsync();


    Task<InventoryCostReportDto>
    GetInventoryCostReportAsync();

    Task<PagedResultDto<InventoryItemDto>>
    GetPagedAsync(
        InventoryPaginationDto paginationDto);

    Task<IEnumerable<InventoryItemDto>>
    GetSortedAsync(
        InventorySortDto sortDto);
}