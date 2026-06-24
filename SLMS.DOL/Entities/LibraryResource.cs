using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SLMS.DOL.Common;

namespace SLMS.DOL.Entities;

public class LibraryResource : BaseEntity
{
    public int CategoryId { get; set; }

    public string ResourceType { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;

    public string Author { get; set; } = string.Empty;

    public string Publisher { get; set; } = string.Empty;

    public string? ISBN { get; set; }

    public int PublicationYear { get; set; }

    public Category Category { get; set; } = null!;
    public ICollection<Request> Requests { get; set; }
    = new List<Request>();

    public ICollection<InventoryItem> InventoryItems { get; set; }
        = new List<InventoryItem>();

    //shelf integration
    public int? ShelfId { get; set; }

    public Shelf? Shelf { get; set; }
}