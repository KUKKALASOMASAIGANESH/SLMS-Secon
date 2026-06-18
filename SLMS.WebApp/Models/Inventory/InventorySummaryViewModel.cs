namespace SLMS.WebApp.Models.Inventory;

public class InventorySummaryViewModel
{
    public int TotalInventoryItems { get; set; }

    public int ActiveInventoryItems { get; set; }

    public int InactiveInventoryItems { get; set; }

    public int TotalShelves { get; set; }
}
