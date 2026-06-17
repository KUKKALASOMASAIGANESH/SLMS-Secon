namespace SLMS.Shared.DTOs.DigitalLibrary;

public class DigitalContentRequestCreateDto
{
    public int EmployeeId { get; set; }

    public int DigitalContentId { get; set; }

    public string Reason { get; set; } = string.Empty;
}