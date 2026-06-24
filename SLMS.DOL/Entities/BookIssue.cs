using SLMS.DOL.Common;

namespace SLMS.DOL.Entities;

public class BookIssue : BaseEntity
{
    public int LibraryResourceId { get; set; }

    public int EmployeeId { get; set; }

    public DateTime IssueDate { get; set; }

    public DateTime DueDate { get; set; }

   // public int IssuedByUserId { get; set; }

    public string Status { get; set; } = "Issued";

    public LibraryResource LibraryResource { get; set; } = null!;

    public Employee Employee { get; set; } = null!;

   // public User IssuedByUser { get; set; } = null!;
}