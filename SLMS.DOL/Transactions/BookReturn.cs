public class BookReturn
{
    public int Id { get; set; }
    public int BookIssueId { get; set; }

    public DateTime ReturnDate { get; set; }
    public decimal FineAmount { get; set; }
}