namespace SLMS.WebApp.Models.Inventory;

public class InventoryDashboardViewModel
{
    public InventorySummaryViewModel Summary
    { get; set; } = new();

    public InventoryCostReportViewModel CostReport
    { get; set; } = new();

    public List<ResourceInventoryReportViewModel>
        ResourceReport
    { get; set; } = new();

    public InventoryAvailabilityReportViewModel
    AvailabilityReport
    { get; set; } = new();


    public List<ShelfReportViewModel>
    ShelfReport
    { get; set; } = new();
}
