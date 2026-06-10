using SLMS.DOL.Common;

namespace SLMS.DOL.Entities;

public class User : BaseEntity
{
    public int EmployeeId { get; set; }

    public string Username { get; set; } = string.Empty;

    public string PasswordHash { get; set; } = string.Empty;

    public DateTime? LastLoginDate { get; set; }

    public Employee Employee { get; set; } = null!;

    public ICollection<UserRole> UserRoles { get; set; }
        = new List<UserRole>();
    public ICollection<CustodyHistory> CustodyTransfers { get; set; }
    = new List<CustodyHistory>();
    public ICollection<Notification> Notifications { get; set; }
    = new List<Notification>();

    public ICollection<AuditLog> AuditLogs { get; set; }
        = new List<AuditLog>();

    public ICollection<DigitalContent> UploadedContents { get; set; }
        = new List<DigitalContent>();
}