namespace SLMS.Shared.DTOs.Inventory;

public class ResourceInventoryReportDto
{
    public int ResourceId { get; set; }

    public string Title { get; set; } = string.Empty;

    public int TotalCopies { get; set; }
}
