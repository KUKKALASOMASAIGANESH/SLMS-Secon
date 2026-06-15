namespace SLMS.Shared.DTOs.Request;

public class RequestCreateDto
{
    public int EmployeeId { get; set; }

    public int ResourceId { get; set; }

    public string RequestType { get; set; }
        = string.Empty;

    public string Status { get; set; }
        = "Pending";

    public DateTime RequestDate { get; set; }

    public string? Remarks { get; set; }
}