using SLMS.DOL.Common;

namespace SLMS.DOL.Entities;

public class Shelf : BaseEntity
{
    public string ShelfName { get; set; }
        = string.Empty;

    public int Capacity { get; set; }

    public int CurrentBookCount { get; set; }

    public ICollection<LibraryResource>
    Resources
    { get; set; }
    = new List<LibraryResource>();
}
