using Microsoft.AspNetCore.Mvc.Rendering;

namespace SLMS.WebApp.Models;

public class BookIssueViewModel
{
    public int Id { get; set; }

    public int LibraryResourceId { get; set; }
    public int EmployeeId { get; set; }

    public string BookTitle { get; set; } = string.Empty;

    public string EmployeeName { get; set; } = string.Empty;

    public DateTime IssueDate { get; set; }

    public DateTime? DueDate { get; set; }

    public int IssuedByUserId { get; set; }

    
    public string Status { get; set; } = string.Empty;

    public List<SelectListItem> EmployeeList { get; set; }
        = new();

    public List<SelectListItem> BookList { get; set; }
        = new();
}