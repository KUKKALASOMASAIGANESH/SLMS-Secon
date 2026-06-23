    using SLMS.DOL.Common;

namespace SLMS.DOL.Entities;

public class BookReturn : BaseEntity
{
    public int BookIssueId { get; set; }

    public DateTime ReturnDate { get; set; }

    public string Condition { get; set; } = string.Empty;

    public string? Remarks { get; set; }

    public decimal FineAmount { get; set; }

    public int ReturnedByUserId { get; set; }

    public BookIssue BookIssue { get; set; } = null!;

    public User ReturnedByUser { get; set; } = null!;
}