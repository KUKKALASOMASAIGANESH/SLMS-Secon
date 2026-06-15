using SLMS.Shared.DTOs.InventoryItem;

namespace SLMS.BLL.Interfaces;

public interface IInventoryItemService
{
    Task<IEnumerable<InventoryItemResponseDto>>
        GetAllAsync();

    Task<InventoryItemResponseDto?>
        GetByIdAsync(int id);

    Task<InventoryItemResponseDto>
        CreateAsync(
            InventoryItemCreateDto dto);

    Task<InventoryItemResponseDto?>
        UpdateAsync(
            int id,
            InventoryItemUpdateDto dto);

    Task<bool>
        DeleteAsync(int id);
}