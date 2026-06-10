using SLMS.DOL.Common;

namespace SLMS.DOL.Entities;

public class Category : BaseEntity
{
    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public ICollection<LibraryResource> Resources { get; set; }
        = new List<LibraryResource>();
}