namespace SLMS.WebApp.Models.DigitalLibrary;

public class AdminRequestViewModel
{
    public int Id { get; set; }

    public string EmployeeName { get; set; } = string.Empty;

    public string ContentTitle { get; set; } = string.Empty;

    public string ApprovalStatus { get; set; } = string.Empty;

    public DateTime RequestDate { get; set; }
}