using SLMS.DOL.Common;

namespace SLMS.DOL.Entities;

public class DigitalContent : BaseEntity
{
    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string Category { get; set; } = string.Empty;

    public string FilePath { get; set; } = string.Empty;

    public int UploadedByUserId { get; set; }

    public User UploadedByUser { get; set; } = null!;

    public ICollection<DigitalContentRequest> Requests { get; set; }
        = new List<DigitalContentRequest>();
}