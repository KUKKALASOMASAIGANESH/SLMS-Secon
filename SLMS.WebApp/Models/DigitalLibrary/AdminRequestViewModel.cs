namespace SLMS.WebApp.Models.DigitalLibrary;

public class AdminRequestViewModel
{
    public int Id { get; set; }

    public int EmployeeId { get; set; }

    public int DigitalContentId { get; set; }

    public string ApprovalStatus { get; set; } = string.Empty;

    public DateTime RequestDate { get; set; }
}