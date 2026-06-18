namespace SLMS.Shared.DTOs.Inventory;

public class InventorySearchDto
{
    public string? AccessionNumber { get; set; }

    public string? InventoryNumber { get; set; }

    public string? ShelfNumber { get; set; }

    public int? ResourceId { get; set; }

    public string? Title { get; set; }

    public string? Author { get; set; }

    public string? Publisher { get; set; }

    public decimal? MinPrice { get; set; }

    public decimal? MaxPrice { get; set; }
}
