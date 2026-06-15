using Microsoft.EntityFrameworkCore;
using SLMS.DAL.Data;
using SLMS.DAL.Repositories.Interfaces;
using SLMS.DOL.Entities;
using SLMS.Shared.DTOs.Inventory;

namespace SLMS.DAL.Repositories.Implementations;

public class InventoryRepository
    : Repository<InventoryItem>,
      IInventoryRepository
{
    public InventoryRepository(
        SLMSDbContext context)
        : base(context)
    {
    }

    public async Task<bool> AccessionNumberExistsAsync(
        string accessionNumber)
    {
        return await _context.InventoryItems
            .AnyAsync(x =>
                x.AccessionNumber == accessionNumber);
    }

    public async Task<bool> InventoryNumberExistsAsync(
        string inventoryNumber)
    {
        return await _context.InventoryItems
            .AnyAsync(x =>
                x.InventoryNumber == inventoryNumber);
    }

    public async Task<bool> ResourceExistsAsync(
        int resourceId)
    {
        return await _context.LibraryResources
            .AnyAsync(x =>
                x.Id == resourceId);
    }


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


    public async Task<IEnumerable<InventoryItem>>
    GetByShelfAsync(string shelfNumber)
    {
        return await _context.InventoryItems
            .Where(x => x.ShelfNumber == shelfNumber)
            .ToListAsync();
    }

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
}