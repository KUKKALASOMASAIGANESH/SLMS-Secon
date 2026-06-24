namespace SLMS.Shared.DTOs.DigitalLibrary;

public class DigitalContentRequestResponseDto
{
    public int Id { get; set; }

    public int EmployeeId { get; set; }

    public int DigitalContentId { get; set; }

    public string EmployeeName { get; set; } = string.Empty;

    public string ContentTitle { get; set; } = string.Empty;

    public string Reason { get; set; } = string.Empty;

    public DateTime RequestDate { get; set; }

    public string ApprovalStatus { get; set; } = string.Empty;
}