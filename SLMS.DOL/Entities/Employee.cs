using SLMS.DOL.Common;

namespace SLMS.DOL.Entities;

public class Employee : BaseEntity
{
    public string EmployeeNumber { get; set; } = string.Empty;

    public string FullName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Phone { get; set; } = string.Empty;

    public string Designation { get; set; } = string.Empty;

    public int DepartmentId { get; set; }

    public Department? Department { get; set; }

    public ICollection<BookIssue> BookIssues { get; set; }
        = new List<BookIssue>();

    public ICollection<Request> Requests { get; set; }
        = new List<Request>();
}