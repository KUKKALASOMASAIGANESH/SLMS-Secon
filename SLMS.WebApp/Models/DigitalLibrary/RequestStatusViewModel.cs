namespace SLMS.WebApp.Models.DigitalLibrary;

public class RequestStatusViewModel
{
    public int Id { get; set; }
    public int DigitalContentId { get; set; }

    public string ApprovalStatus { get; set; }
        = string.Empty;
}