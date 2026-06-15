public interface ITransactionService
{
    BookIssue IssueBook(int employeeId, int resourceId);
    object ReturnBook(int issueId);
    List<BookIssue> GetAllIssues();
}