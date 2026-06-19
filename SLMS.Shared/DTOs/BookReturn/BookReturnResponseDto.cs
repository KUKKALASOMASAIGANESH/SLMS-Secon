namespace SLMS.Shared.DTOs.BookReturn;

public class BookReturnResponseDto
{
    public int Id { get; set; }

    public int BookIssueId { get; set; }

    public DateTime ReturnDate { get; set; }

    public string Condition { get; set; } = string.Empty;

    public string? Remarks { get; set; }

    public decimal FineAmount { get; set; }

    public int ReturnedByUserId { get; set; }
}