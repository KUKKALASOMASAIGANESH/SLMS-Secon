namespace SLMS.WebApp.Models.Inventory;

public class ResourceInventoryReportViewModel
{
    public int ResourceId { get; set; }

    public string Title { get; set; }
        = string.Empty;

    public int TotalCopies { get; set; }
}