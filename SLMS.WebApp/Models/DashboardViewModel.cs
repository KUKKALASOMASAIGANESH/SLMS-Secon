namespace SLMS.WebApp.Models;

public class DashboardViewModel
{
    public int TotalDepartments { get; set; }

    public int TotalEmployees { get; set; }

    public int TotalCategories { get; set; }

    public int TotalResources { get; set; }

    public int TotalShelves { get; set; }

    public int TotalIssuedBooks { get; set; }

    public int TotalReturnedBooks { get; set; }

    public int TotalOverdueBooks { get; set; }

    public int TotalDigitalContents { get; set; }

    public int TotalRequests { get; set; }

    public int TotalUsers { get; set; }

    public int TotalAuditLogs { get; set; }

    // Analytics
    public string MostBorrowedBook { get; set; } = string.Empty;

    public string LeastBorrowedBook { get; set; } = string.Empty;

    public string MostActiveEmployee { get; set; } = string.Empty;

    public string MostActiveDepartment { get; set; } = string.Empty;

    public string Recommendation { get; set; } = string.Empty;
}