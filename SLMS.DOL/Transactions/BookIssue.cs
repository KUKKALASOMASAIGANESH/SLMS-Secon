public class BookIssue
{
    public int Id { get; set; }
    public int BookId { get; set; }
    public int UserId { get; set; }
    public int InventoryItemId { get; set; }

    public DateTime IssueDate { get; set; }
    public DateTime DueDate { get; set; }

    public bool IsReturned { get; set; }
}