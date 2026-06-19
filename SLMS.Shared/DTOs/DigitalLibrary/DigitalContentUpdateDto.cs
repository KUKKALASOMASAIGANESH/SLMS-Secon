namespace SLMS.Shared.DTOs.DigitalLibrary;

public class DigitalContentUpdateDto
{
    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string ContentType { get; set; } = string.Empty;

    public string FilePath { get; set; } = string.Empty;
}