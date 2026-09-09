namespace SLMS.Shared.DTOs;

public class DashboardAnalyticsDto
{
    // Statistics
    public int TotalDepartments { get; set; }
    public int TotalEmployees { get; set; }
    public int TotalResources { get; set; }
    public int TotalCategories { get; set; }
    public int TotalIssuedBooks { get; set; }
    public int TotalReturnedBooks { get; set; }
    public int TotalDigitalContents { get; set; }

    // Analytics
    public string MostBorrowedBook { get; set; } = "";

    public string LeastBorrowedBook { get; set; } = "";

    public string MostActiveEmployee { get; set; } = "";

    public string MostActiveDepartment { get; set; } = "";

    public int OverdueBooks { get; set; }

    public string Recommendation { get; set; } = "";
}