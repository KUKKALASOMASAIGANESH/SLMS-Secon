using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SLMS.DOL.Common;

namespace SLMS.DOL.Entities;

public class InventoryItem : BaseEntity
{
    public int ResourceId { get; set; }

    public string AccessionNumber { get; set; } = string.Empty;

    public string InventoryNumber { get; set; } = string.Empty;

    public string ShelfNumber { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public LibraryResource Resource { get; set; } = null!;
    public ICollection<CustodyHistory> CustodyHistories { get; set; }
    = new List<CustodyHistory>();
    public ICollection<BookIssue> BookIssues { get; set; }
    = new List<BookIssue>();
}