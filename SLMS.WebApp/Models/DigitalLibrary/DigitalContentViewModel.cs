namespace SLMS.WebApp.Models.DigitalLibrary;

public class DigitalContentViewModel
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string Category { get; set; } = string.Empty;

    public string FilePath { get; set; } = string.Empty;

    public string Author { get; set; } = string.Empty;

    public bool HasApprovedAccess { get; set; }

}