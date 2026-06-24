using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;

namespace SLMS.WebApp.Models;

public class LibraryResourceViewModel
{
    public int Id { get; set; }

    public int CategoryId { get; set; }

    public string ResourceType { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;

    public string Author { get; set; } = string.Empty;

    public string Publisher { get; set; } = string.Empty;

    public string ISBN { get; set; } = string.Empty;

    public int PublicationYear { get; set; }

    //shelf integration
    public int? ShelfId { get; set; }
    public List<SelectListItem>? Shelves { get; set; }
    public string? ShelfName { get; set; }




    // shelf


    [NotMapped]
    public List<SelectListItem>? CategoryList { get; set; }
}