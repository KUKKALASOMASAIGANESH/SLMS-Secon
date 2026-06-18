namespace SLMS.WebApp.Models;

public class BookIssueViewModel
{
    public int Id { get; set; }

    public int InventoryItemId { get; set; }

    public int EmployeeId { get; set; }

    public DateTime IssueDate { get; set; }

    public DateTime DueDate { get; set; }

    public int IssuedByUserId { get; set; }

    public string Status { get; set; } = string.Empty;
}