namespace SLMS.Shared.DTOs.BookIssue;

public class BookIssueCreateDto
{
    public int LibraryResourceId { get; set; }

    public int EmployeeId { get; set; }

    public DateTime IssueDate { get; set; }

    public DateTime DueDate { get; set; }

    public int IssuedByUserId { get; set; }

    public string Status { get; set; } = "Issued";
}