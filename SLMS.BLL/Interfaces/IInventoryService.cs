using SLMS.Shared.DTOs.Inventory;

namespace SLMS.BLL.Interfaces;

public interface IInventoryService
{
    Task<IEnumerable<InventoryItemDto>> GetAllAsync();

    Task<InventoryItemDto?> GetByIdAsync(int id);

    Task CreateAsync(CreateInventoryItemDto dto);

    Task<bool> UpdateAsync(
    int id,
    UpdateInventoryItemDto dto);
    Task<bool> DeleteAsync(int id);
}