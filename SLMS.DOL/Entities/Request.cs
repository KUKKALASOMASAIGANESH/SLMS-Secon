using SLMS.DOL.Common;

namespace SLMS.DOL.Entities;

public class Request : BaseEntity
{
    // Remove Id if BaseEntity already has it
    // public int Id { get; set; }

    public int EmployeeId { get; set; }

    public int ResourceId { get; set; }

    public string RequestType { get; set; } = string.Empty;

    public string Status { get; set; } = "Pending";

    public DateTime RequestDate { get; set; }

    public string? Remarks { get; set; }

    public Employee? Employee { get; set; } 

    public LibraryResource? Resource { get; set; }
}