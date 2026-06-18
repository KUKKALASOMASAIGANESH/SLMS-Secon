namespace SLMS.WebApp.Models.Inventory;

public class InventoryCostReportViewModel
{
    public decimal TotalInventoryValue { get; set; }

    public decimal AverageInventoryValue { get; set; }

    public decimal HighestPrice { get; set; }

    public decimal LowestPrice { get; set; }
}