namespace SLMS.Shared.DTOs.Shelf;

public class ShelfResponseDto
{
    public int Id { get; set; }

    public string ShelfName { get; set; }
        = string.Empty;

    public int Capacity { get; set; }

    public int CurrentBookCount { get; set; }

    public bool IsActive { get; set; }

    public List<ShelfResourceDto>
    Resources
    { get; set; }
    = new();
}