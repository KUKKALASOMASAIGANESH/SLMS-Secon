namespace SLMS.Shared.DTOs.Inventory;

public class InventorySortDto
{
    public string? SortBy { get; set; }

    public bool Descending { get; set; } = false;
}