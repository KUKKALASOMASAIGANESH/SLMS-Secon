namespace SLMS.WebApp.Models.DigitalLibrary;

public class DownloadHistoryApiResponse
{
    public DigitalContentApiResponse? DigitalContent { get; set; }

    public DateTime DownloadedOn { get; set; }
}

public class DigitalContentApiResponse
{
    public string Title { get; set; } = string.Empty;
}