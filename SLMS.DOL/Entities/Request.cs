using SLMS.DOL.Common;

namespace SLMS.DOL.Entities;

public class Request : BaseEntity
{
    public int EmployeeId { get; set; }

    public int ResourceId { get; set; }

    public string RequestType { get; set; } = string.Empty;

    public string Status { get; set; } = "Pending";

    public DateTime RequestDate { get; set; }

    public string? Remarks { get; set; }

    public Employee Employee { get; set; } = null!;

    public LibraryResource Resource { get; set; } = null!;
}