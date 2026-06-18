namespace SLMS.WebApp.Models.Inventory;

public class InventoryViewModel
{
    public int Id { get; set; }

    public int ResourceId { get; set; }

    public string AccessionNumber { get; set; }
        = string.Empty;

    public string InventoryNumber { get; set; }
        = string.Empty;

    public string ShelfNumber { get; set; }
        = string.Empty;

    public decimal Price { get; set; }

    public bool IsActive { get; set; }
}