namespace SLMS.Shared.DTOs.LibraryResource;

public class LibraryResourceCreateDto
{
    public int CategoryId { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Author { get; set; } = string.Empty;

    public string Publisher { get; set; } = string.Empty;

    public string? ISBN { get; set; }

    public int PublicationYear { get; set; }
}