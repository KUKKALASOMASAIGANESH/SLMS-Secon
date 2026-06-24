using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLMS.Shared.DTOs.Custody;

public class CustodyHistoryReportDto
{
    public string ResourceTitle { get; set; }
        = string.Empty;

    public string EmployeeName { get; set; }
        = string.Empty;

    public string DepartmentName { get; set; }
        = string.Empty;

    public DateTime IssueDate { get; set; }

    public DateTime? ReturnDate { get; set; }

    public string Action { get; set; }
        = string.Empty;

    public string Status { get; set; }
        = string.Empty;
}