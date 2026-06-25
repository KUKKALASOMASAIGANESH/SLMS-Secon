namespace SLMS.Shared.DTOs.Shelf;

public class ShelfResourceDto
{
    public int Id { get; set; }

    public string Title { get; set; }
        = string.Empty;

    public string Author { get; set; }
        = string.Empty;

    public string ResourceType { get; set; }
        = string.Empty;
}