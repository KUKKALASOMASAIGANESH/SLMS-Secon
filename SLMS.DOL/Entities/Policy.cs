using SLMS.DOL.Common;

namespace SLMS.DOL.Entities;

public class Policy : BaseEntity
{
    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string? AttachmentPath { get; set; }

    public DateTime EffectiveDate { get; set; }
}