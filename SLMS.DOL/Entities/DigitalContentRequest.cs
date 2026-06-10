using SLMS.DOL.Common;

namespace SLMS.DOL.Entities;

public class DigitalContentRequest : BaseEntity
{
    public int DigitalContentId { get; set; }

    public int EmployeeId { get; set; }

    public string ApprovalStatus { get; set; } = "Pending";

    public DateTime RequestDate { get; set; }

    public int? ApprovedByUserId { get; set; }

    public DateTime? ApprovedDate { get; set; }

    public DigitalContent DigitalContent { get; set; } = null!;

    public Employee Employee { get; set; } = null!;

    public User? ApprovedByUser { get; set; }
}