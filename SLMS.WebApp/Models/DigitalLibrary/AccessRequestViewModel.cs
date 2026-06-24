namespace SLMS.WebApp.Models.DigitalLibrary;

public class AccessRequestViewModel
{
    public int EmployeeId { get; set; }

    public int DigitalContentId { get; set; }

    public string ApprovalStatus { get; set; }
        = string.Empty;
}