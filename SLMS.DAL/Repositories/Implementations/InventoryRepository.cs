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
}
