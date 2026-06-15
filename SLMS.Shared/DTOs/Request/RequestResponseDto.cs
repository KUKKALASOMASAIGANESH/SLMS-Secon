namespace SLMS.Shared.DTOs.Request;

public class RequestResponseDto
{
    public int Id { get; set; }

    public int EmployeeId { get; set; }

    public int ResourceId { get; set; }

    public string RequestType { get; set; }
        = string.Empty;

    public string Status { get; set; }
        = string.Empty;

    public DateTime RequestDate { get; set; }

    public string? Remarks { get; set; }
}