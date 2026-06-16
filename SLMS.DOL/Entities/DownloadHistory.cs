using SLMS.DOL.Common;

namespace SLMS.DOL.Entities;

public class DownloadHistory : BaseEntity
{
    public int EmployeeId { get; set; }

    public Employee Employee { get; set; } = null!;

    public int DigitalContentId { get; set; }

    public DigitalContent DigitalContent { get; set; } = null!;

    public DateTime DownloadedOn { get; set; }
        = DateTime.UtcNow;
}