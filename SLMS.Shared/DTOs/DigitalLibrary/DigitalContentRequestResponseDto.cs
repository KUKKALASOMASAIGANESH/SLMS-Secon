namespace SLMS.Shared.DTOs.DigitalLibrary;

public class DigitalContentRequestResponseDto
{
    public int Id { get; set; }

    public int EmployeeId { get; set; }

    public int DigitalContentId { get; set; }

    public string Reason { get; set; } = string.Empty;

    public DateTime RequestedOn { get; set; }

    public string Status { get; set; } = string.Empty;
    // Pending, Approved, Rejected
}