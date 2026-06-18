namespace SLMS.Shared.DTOs.Inventory;

public class InventorySummaryDto
{
    public int TotalInventoryItems { get; set; }

    public int ActiveInventoryItems { get; set; }

    public int InactiveInventoryItems { get; set; }

    public int TotalShelves { get; set; }
}
