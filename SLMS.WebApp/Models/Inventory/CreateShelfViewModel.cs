using System.ComponentModel.DataAnnotations;

namespace SLMS.WebApp.Models.Inventory;

public class CreateShelfViewModel
{
    [Required(ErrorMessage = "Shelf Name is required")]
    [StringLength(100,
        ErrorMessage = "Shelf Name cannot exceed 100 characters")]
    public string ShelfName { get; set; }
        = string.Empty;

    [Required(ErrorMessage = "Capacity is required")]
    [Range(1, 10000,
        ErrorMessage = "Capacity must be between 1 and 10000")]
    public int Capacity { get; set; }

    [Required(ErrorMessage = "Current Book Count is required")]
    [Range(0, 10000,
        ErrorMessage = "Current Book Count must be between 0 and 10000")]
    public int CurrentBookCount { get; set; }
}