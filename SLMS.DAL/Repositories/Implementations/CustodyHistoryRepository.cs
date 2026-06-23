using Microsoft.EntityFrameworkCore;
using SLMS.DAL.Data;
using SLMS.DAL.Repositories.Interfaces;
using SLMS.DOL.Entities;

namespace SLMS.DAL.Repositories.Implementations;

public class CustodyHistoryRepository
    : Repository<CustodyHistory>, ICustodyHistoryRepository
{
    public CustodyHistoryRepository(
        SLMSDbContext context)
        : base(context)
    {
    }

    public async Task<IEnumerable<CustodyHistory>>
        GetByInventoryItemAsync(int inventoryItemId)
    {
        return await _dbSet
    .Include(x => x.FromDepartment)
    .Include(x => x.ToDepartment)
    .Include(x => x.InventoryItem)
    .Where(x => x.InventoryItemId == inventoryItemId)
    .OrderBy(x => x.TransferDate)
    .ToListAsync();
    }
    public async Task<CustodyHistory?>
GetCurrentCustodianAsync(int inventoryItemId)
    {
        return await _dbSet
            .Include(x => x.FromDepartment)
            .Include(x => x.ToDepartment)
            .Include(x => x.InventoryItem)
            .Where(x => x.InventoryItemId == inventoryItemId)
            .OrderByDescending(x => x.TransferDate)
            .ThenByDescending(x => x.Id)
            .FirstOrDefaultAsync();
    }
    public async Task<IEnumerable<CustodyHistory>>
    GetAllWithDetailsAsync()
    {
        return await _dbSet
            .Include(x => x.FromDepartment)
            .Include(x => x.ToDepartment)
            .Include(x => x.InventoryItem)
            .ToListAsync();
    }
}