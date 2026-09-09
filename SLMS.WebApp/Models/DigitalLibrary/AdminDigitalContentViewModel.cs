using System.ComponentModel.DataAnnotations;

namespace SLMS.WebApp.Models.DigitalLibrary;

public class AdminDigitalContentViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Title is required")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Content Type is required")]
    public string ContentType { get; set; } = string.Empty;

    [Required(ErrorMessage = "Author is required")]
    public string Author { get; set; } = string.Empty;

    [Required(ErrorMessage = "File Path is required")]
    public string FilePath { get; set; } = string.Empty;

    public string? Description { get; set; }
}