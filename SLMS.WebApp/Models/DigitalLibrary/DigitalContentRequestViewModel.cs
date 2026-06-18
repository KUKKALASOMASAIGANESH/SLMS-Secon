namespace SLMS.WebApp.Models.DigitalLibrary;

public class DigitalContentRequestViewModel
{
    public int DigitalContentId { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Reason { get; set; } = string.Empty;
}