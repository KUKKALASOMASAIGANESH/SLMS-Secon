/*
 * IInventoryService
 *
 * Purpose:
 * Defines the business operations available for the Inventory module.
 *
 * Responsibilities:
 * - Inventory item management (CRUD)
 * - Inventory search and filtering
 * - Shelf management operations
 * - Inventory reporting
 * - Availability tracking
 * - Pagination and sorting
 * - Soft delete and restore functionality
 *
 * Architecture Flow:
 * InventoryController
 *        ↓
 * IInventoryService
 *        ↓
 * InventoryService
 *        ↓
 * InventoryRepository
 *        ↓
 * Database
 *
 * This interface acts as a contract between the API layer
 * and the business logic layer, promoting loose coupling
 * and dependency injection.
 */

using SLMS.Shared.DTOs.Inventory;
using SLMS.Shared.DTOs.Common;

namespace SLMS.BLL.Interfaces;
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

    Task<IEnumerable<InventoryItemDto>>
    GetAvailableBooksAsync();

    Task<IEnumerable<InventoryItemDto>>
        GetUnavailableBooksAsync();

    Task<AvailabilityReportDto>
        GetAvailabilityReportAsync();

    Task<bool> RestoreAsync(int id);

    Task<IEnumerable<InventoryItemDto>>
    GetInactiveAsync();
}