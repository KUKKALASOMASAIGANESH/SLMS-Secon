using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SLMS.DOL.Common;

namespace SLMS.DOL.Entities;

public class Department : BaseEntity
{
    public string DepartmentCode { get; set; } = string.Empty;

    public string DepartmentName { get; set; } = string.Empty;

    public string? Description { get; set; }

    public ICollection<Employee> Employees { get; set; }
        = new List<Employee>();
    public ICollection<CustodyHistory> CustodyTransfersFrom { get; set; }
    = new List<CustodyHistory>();

    public ICollection<CustodyHistory> CustodyTransfersTo { get; set; }
        = new List<CustodyHistory>();
}