namespace SLMS.Shared.DTOs.DigitalLibrary;

public class DigitalContentCreateDto
{
    public string Title { get; set; } = string.Empty;

    public string ContentType { get; set; } = string.Empty;
    // EBook, EJournal, Presentation, Training Material


    public string FilePath { get; set; } = string.Empty;

    public string? Description { get; set; }
}