namespace SLMS.Shared.DTOs.Inventory;

public class InventoryCostReportDto
{
    public decimal TotalInventoryValue { get; set; }

    public decimal AverageInventoryValue { get; set; }

    public decimal HighestPrice { get; set; }

    public decimal LowestPrice { get; set; }
}