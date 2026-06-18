namespace SLMS.Shared.DTOs.Inventory;

public class UpdateInventoryItemDto
{
    public int ResourceId { get; set; }
    public string AccessionNumber { get; set; } = string.Empty;

    public string InventoryNumber { get; set; } = string.Empty;

    public string ShelfNumber { get; set; } = string.Empty;

    public decimal Price { get; set; }
}