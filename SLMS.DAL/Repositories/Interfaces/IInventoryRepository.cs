/*
 * IInventoryRepository
 *
 * Purpose:
 * Defines data access operations for the Inventory module.
 *
 * Responsibilities:
 * - Inventory item persistence and retrieval
 * - Inventory search and filtering queries
 * - Shelf management queries
 * - Inventory reporting queries
 * - Availability tracking queries
 * - Inventory validation checks
 *
 * Architecture Flow:
 * InventoryController
 *        ↓
 * InventoryService
 *        ↓
 * IInventoryRepository
 *        ↓
 * InventoryRepository
 *        ↓
 * Entity Framework Core
 *        ↓
 * PostgreSQL Database
 *
 * This interface acts as a contract between the business
 * logic layer and the data access layer, ensuring database
 * operations remain isolated from business rules.
 */


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

    Task<IEnumerable<InventoryItem>>
    GetAvailableBooksAsync();

    Task<IEnumerable<InventoryItem>>
        GetUnavailableBooksAsync();

    Task<AvailabilityReportDto>
        GetAvailabilityReportAsync();

}
