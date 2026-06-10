using SLMS.DOL.Common;

namespace SLMS.DOL.Entities;

public class Notification : BaseEntity
{
    public int UserId { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Message { get; set; } = string.Empty;

    public bool IsRead { get; set; }

    public User User { get; set; } = null!;
}