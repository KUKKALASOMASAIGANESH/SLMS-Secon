using SLMS.DAL.Data;
using SLMS.DAL.Repositories.Interfaces;
using SLMS.DOL.Entities;

namespace SLMS.DAL.Repositories.Implementations;

public class InventoryItemRepository
    : Repository<InventoryItem>,
      IInventoryItemRepository
{
    public InventoryItemRepository(
        SLMSDbContext context)
        : base(context)
    {
    }
}