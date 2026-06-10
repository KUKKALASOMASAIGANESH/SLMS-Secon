using SLMS.DOL.Common;

namespace SLMS.DOL.Entities;

public class AuditLog : BaseEntity
{
    public int UserId { get; set; }

    public string Module { get; set; } = string.Empty;

    public string Action { get; set; } = string.Empty;

    public string? OldValue { get; set; }

    public string? NewValue { get; set; }

    public string? IPAddress { get; set; }

    public User User { get; set; } = null!;
}