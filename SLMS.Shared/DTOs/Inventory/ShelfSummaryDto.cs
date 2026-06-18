namespace SLMS.Shared.DTOs.Inventory;

public class ShelfSummaryDto
{
    public string ShelfNumber { get; set; } = string.Empty;

    public int BookCount { get; set; }
}