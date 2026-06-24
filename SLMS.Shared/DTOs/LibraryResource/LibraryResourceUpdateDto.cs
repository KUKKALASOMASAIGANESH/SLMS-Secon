namespace SLMS.Shared.DTOs.LibraryResource;

public class LibraryResourceUpdateDto
{
    public int CategoryId { get; set; }

    public string ResourceType { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;

    public string Author { get; set; } = string.Empty;

    public string Publisher { get; set; } = string.Empty;

    public string? ISBN { get; set; }

    public int PublicationYear { get; set; }

    //shelf
    public int? ShelfId { get; set; }
}