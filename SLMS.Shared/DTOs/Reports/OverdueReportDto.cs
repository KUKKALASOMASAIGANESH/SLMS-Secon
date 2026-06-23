using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLMS.Shared.DTOs.Reports
{
    public class OverdueReportDto
    {
        public string BookTitle { get; set; }
        public string EmployeeName { get; set; }
        public int DaysOverdue { get; set; }

    }
}
