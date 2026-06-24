namespace SLMS.Shared.DTOs.BookIssue;

public class BookIssueResponseDto
{
    public int Id { get; set; }

    public int LibraryResourceId { get; set; }

    public int EmployeeId { get; set; }

    public string BookTitle { get; set; } = string.Empty;

    public string EmployeeName { get; set; } = string.Empty;

    public DateTime IssueDate { get; set; }

    public DateTime DueDate { get; set; }

    public int IssuedByUserId { get; set; }

    public string Status { get; set; } = string.Empty;
}