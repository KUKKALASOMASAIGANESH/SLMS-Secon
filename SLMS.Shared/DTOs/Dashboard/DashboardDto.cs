using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLMS.Shared.DTOs.Dashboard;

public class DashboardDto
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
}
