using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLMS.Shared.DTOs.Reports
{
    public class IssueReportDto
    {
        public string BookTitle { get; set; }
        public string EmployeeName { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime DueDate { get; set; }
    }
}
