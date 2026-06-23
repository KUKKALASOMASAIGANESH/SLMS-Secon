namespace SLMS.Shared.DTOs.Shelf;

public class ShelfCreateDto
{
    public string ShelfName { get; set; }
        = string.Empty;

    public int Capacity { get; set; }

    public int CurrentBookCount { get; set; }
}
