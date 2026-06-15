/*
 * InventoryRepository
 *
 * Purpose:
 * Provides data access operations for the Inventory module.
 *
 * Responsibilities:
 * - Inventory item data retrieval and persistence
 * - Inventory search and filtering
 * - Shelf management queries
 * - Inventory reporting queries
 * - Availability tracking queries
 * - Validation checks for inventory creation
 *
 * Flow:
 * Controller
 *    ↓
 * InventoryService
 *    ↓
 * InventoryRepository
 *    ↓
 * Entity Framework Core
 *    ↓
 * PostgreSQL Database
 *
 * This repository acts as the data access layer and isolates
 * database operations from business logic implemented in the Service layer.
 */


using Microsoft.EntityFrameworkCore;
using SLMS.DAL.Data;
using SLMS.DAL.Repositories.Interfaces;
using SLMS.DOL.Entities;
using SLMS.Shared.DTOs.Inventory;

namespace SLMS.DAL.Repositories.Implementations;

// Repository implementation for inventory data access,
// reporting and availability tracking operations.
public class InventoryRepository
    : Repository<InventoryItem>,
      IInventoryRepository
{
    public InventoryRepository(
        SLMSDbContext context)
        : base(context)
    {
    }

    // Checks whether the specified accession number already exists.
    public async Task<bool> AccessionNumberExistsAsync(
        string accessionNumber)
    {
        return await _context.InventoryItems
            .AnyAsync(x =>
                x.AccessionNumber == accessionNumber);
    }

    // Checks whether the specified inventory number already exists.
    public async Task<bool> InventoryNumberExistsAsync(
        string inventoryNumber)
    {
        return await _context.InventoryItems
            .AnyAsync(x =>
                x.InventoryNumber == inventoryNumber);
    }

    // Validates that the referenced library resource exists.
    public async Task<bool> ResourceExistsAsync(
        int resourceId)
    {
        return await _context.LibraryResources
            .AnyAsync(x =>
                x.Id == resourceId);
    }

    // Performs dynamic inventory search based on the supplied filter criteria.
    public async Task<IEnumerable<InventoryItem>> SearchAsync(
    string? accessionNumber,
    string? inventoryNumber,
    string? shelfNumber,
    int? resourceId,
    string? title,
    string? author,
    string? publisher,
    decimal? minPrice,
    decimal? maxPrice)
    {
        // Build query incrementally based on provided filters.
        var query = _context.InventoryItems
            .Include(i => i.Resource)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(accessionNumber))
        {
            query = query.Where(x =>
                x.AccessionNumber.Contains(accessionNumber));
        }

        if (!string.IsNullOrWhiteSpace(inventoryNumber))
        {
            query = query.Where(x =>
                x.InventoryNumber.Contains(inventoryNumber));
        }

        if (!string.IsNullOrWhiteSpace(shelfNumber))
        {
            query = query.Where(x =>
                x.ShelfNumber.Contains(shelfNumber));
        }

        if (resourceId.HasValue)
        {
            query = query.Where(x =>
                x.ResourceId == resourceId.Value);
        }

        if (!string.IsNullOrWhiteSpace(title))
        {
            query = query.Where(x =>
                x.Resource.Title.Contains(title));
        }

        if (!string.IsNullOrWhiteSpace(author))
        {
            query = query.Where(x =>
                x.Resource.Author.Contains(author));
        }

        if (!string.IsNullOrWhiteSpace(publisher))
        {
            query = query.Where(x =>
                x.Resource.Publisher.Contains(publisher));
        }

        if (minPrice.HasValue)
        {
            query = query.Where(x =>
                x.Price >= minPrice.Value);
        }

        if (maxPrice.HasValue)
        {
            query = query.Where(x =>
                x.Price <= maxPrice.Value);
        }

        return await query.ToListAsync();
    }

    // Retrieves inventory items located on a specific shelf.
    public async Task<IEnumerable<InventoryItem>>
    GetByShelfAsync(string shelfNumber)
    {
        return await _context.InventoryItems
            .Where(x => x.ShelfNumber == shelfNumber)
            .ToListAsync();
    }

    // Generates shelf-wise inventory distribution statistics.
    public async Task<IEnumerable<ShelfSummaryDto>>
    GetShelfSummaryAsync()
    {
        return await _context.InventoryItems
            .GroupBy(x => x.ShelfNumber)
            .Select(g => new ShelfSummaryDto
            {
                ShelfNumber = g.Key,
                BookCount = g.Count()
            })
            .ToListAsync();
    }

    // Generates overall inventory statistics.
    public async Task<InventorySummaryDto>
    GetInventorySummaryAsync()
    {
        return new InventorySummaryDto
        {
            TotalInventoryItems =
                await _context.InventoryItems.CountAsync(),

            ActiveInventoryItems =
                await _context.InventoryItems
                    .CountAsync(x => x.IsActive),

            InactiveInventoryItems =
                await _context.InventoryItems
                    .CountAsync(x => !x.IsActive),

            TotalShelves =
                await _context.InventoryItems
                    .Select(x => x.ShelfNumber)
                    .Distinct()
                    .CountAsync()
        };
    }

    // Generates resource-wise inventory report with copy counts.
    public async Task<IEnumerable<ResourceInventoryReportDto>>
    GetResourceInventoryReportAsync()
    {
        return await _context.InventoryItems
            .Include(x => x.Resource)
            .GroupBy(x => new
            {
                x.ResourceId,
                x.Resource.Title
            })
            .Select(g =>
                new ResourceInventoryReportDto
                {
                    ResourceId = g.Key.ResourceId,
                    Title = g.Key.Title,
                    TotalCopies = g.Count()
                })
            .ToListAsync();
    }

    // Generates inventory valuation and cost analysis metrics
    public async Task<InventoryCostReportDto>
    GetInventoryCostReportAsync()
    {
        var items = _context.InventoryItems;

        return new InventoryCostReportDto
        {
            TotalInventoryValue =
                await items.SumAsync(x => x.Price),

            AverageInventoryValue =
                await items.AverageAsync(x => x.Price),

            HighestPrice =
                await items.MaxAsync(x => x.Price),

            LowestPrice =
                await items.MinAsync(x => x.Price)
        };
    }

    // Retrieves inventory items currently marked as active and available.
    public async Task<IEnumerable<InventoryItem>>
    GetAvailableBooksAsync()
    {
        return await _context.InventoryItems
            .Where(x => x.IsActive)
            .ToListAsync();
    }


    // Placeholder implementation.
    // Will be updated when issue/return transaction tracking is available.
    public async Task<IEnumerable<InventoryItem>>
    GetUnavailableBooksAsync()
    {
        return Enumerable.Empty<InventoryItem>();
    }

    // Generates availability statistics for inventory items.
    public async Task<AvailabilityReportDto>
    GetAvailabilityReportAsync()
    {
        var available =
            await _context.InventoryItems
                .CountAsync(x => x.IsActive);

        return new AvailabilityReportDto
        {
            AvailableBooks = available,
            UnavailableBooks = 0
        };
    }
}