namespace SLMS.WebApp.Models;

public class CustodyHistoryReportViewModel
{
    public DateTime Date { get; set; }

    public string ResourceTitle { get; set; }
        = string.Empty;

    public string EmployeeName { get; set; }
        = string.Empty;

    public string DepartmentName { get; set; }
        = string.Empty;

    public string Action { get; set; }
        = string.Empty;

    public string Status { get; set; }
        = string.Empty;
}