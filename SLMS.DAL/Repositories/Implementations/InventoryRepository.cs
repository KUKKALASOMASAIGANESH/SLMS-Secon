using Microsoft.EntityFrameworkCore;
using SLMS.DAL.Data;
using SLMS.DAL.Repositories.Interfaces;
using SLMS.DOL.Entities;

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
}