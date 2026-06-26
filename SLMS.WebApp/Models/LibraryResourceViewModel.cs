using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SLMS.WebApp.Models;

public class LibraryResourceViewModel
{
    // Primary Key
    public int Id { get; set; }

    // Category Selection
    [Required(ErrorMessage = "Category is required")]
    public int CategoryId { get; set; }

    // Resource Type (Book, Journal, Magazine)
    [Required(ErrorMessage = "Resource Type is required")]
    public string ResourceType { get; set; } = string.Empty;

    // Resource Title
    [Required(ErrorMessage = "Title is required")]
    [StringLength(200)]
    public string Title { get; set; } = string.Empty;

    // Author Name
    [Required(ErrorMessage = "Author is required")]
    [StringLength(150)]
    public string Author { get; set; } = string.Empty;

    // Publisher Name
    [Required(ErrorMessage = "Publisher is required")]
    [StringLength(150)]
    public string Publisher { get; set; } = string.Empty;

    // ISBN Number
    [Required(ErrorMessage = "ISBN is required")]
    [StringLength(20)]
    public string ISBN { get; set; } = string.Empty;

    // Publication Year Validation
    [Required(ErrorMessage = "Publication Year is required")]
    [Range(1900, 2100, ErrorMessage = "Enter a valid publication year")]
    public int PublicationYear { get; set; }

    // Shelf Selection
    [Required(ErrorMessage = "Shelf is required")]
    public int? ShelfId { get; set; }

    // Dropdown List for Shelf
    public List<SelectListItem>? Shelves { get; set; }

    // Display Shelf Name in Index/Details
    public string? ShelfName { get; set; }

    // Display Category Name in Index/Details
    public string CategoryName { get; set; } = string.Empty;

    // Dropdown List for Category
    [NotMapped]
    public List<SelectListItem>? CategoryList { get; set; }
}