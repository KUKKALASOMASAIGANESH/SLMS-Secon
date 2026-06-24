namespace SLMS.Shared.DTOs.LibraryResource;

public class LibraryResourceResponseDto
{
    public int Id { get; set; }

    public int CategoryId { get; set; }

    public string ResourceType { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;

    public string Author { get; set; } = string.Empty;

    public string Publisher { get; set; } = string.Empty;

    public string? ISBN { get; set; }

    public int PublicationYear { get; set; }

    // Shelf
    public int? ShelfId { get; set; }

    public string? ShelfName { get; set; }

    public string CategoryName { get; set; } = string.Empty;
}