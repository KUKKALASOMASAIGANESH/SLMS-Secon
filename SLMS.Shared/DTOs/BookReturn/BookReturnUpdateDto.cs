namespace SLMS.Shared.DTOs.BookReturn;

public class BookReturnUpdateDto
{
    public int BookIssueId { get; set; }

    public DateTime ReturnDate { get; set; }

    public string Condition { get; set; } = string.Empty;

    public string? Remarks { get; set; }

    public decimal FineAmount { get; set; }

    public int ReturnedByUserId { get; set; }
}