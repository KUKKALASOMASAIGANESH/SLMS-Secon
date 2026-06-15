using Microsoft.VisualBasic;
using SLMS.DAL;
using SLMS.DAL.Data;
using SLMS.DOL.Entities;

public class TransactionService
{
    private readonly SLMSDbContext _context;

    public TransactionService(SLMSDbContext context)
    {
        _context = context;
    }

    // ✅ Issue Book
    public string IssueBook(int bookId, int userId)
    {
        try
        {
            var issue = new BookIssue
            {
                BookId = bookId,
                UserId = userId,
                IssueDate = DateTime.UtcNow,
                DueDate = DateTime.UtcNow.AddDays(7),
                IsReturned = false
            };

            _context.BookIssues.Add(issue);
            _context.SaveChanges();

            return "SUCCESS";
        }
        catch (Exception ex)
        {
            return ex.InnerException?.Message ?? ex.Message;
        }
    }
   
    public List<BookIssue> GetOverdueBooks()
    {
        return _context.BookIssues
            .Where(x => x.DueDate < DateTime.UtcNow && !x.IsReturned)
            .ToList();
    }
    public Request CreateRequest(Request req)
    {
        req.Status = "Pending";
        _context.Requests.Add(req);
        _context.SaveChanges();
        return req;
    }
    public object ReturnBook(int bookIssueId)
    {
        try
        {
            var issue = _context.BookIssues
                .FirstOrDefault(x => x.Id == bookIssueId);

            if (issue == null)
                return "Record not found";

            issue.IsReturned = true;

            DateTime returnDate = DateTime.UtcNow;
            int fine = 0;

            if (returnDate > issue.DueDate)
            {
                int daysLate = (returnDate - issue.DueDate).Days;
                fine = daysLate * 10;
            }

            _context.SaveChanges();

            return new
            {
                message = "Book Returned Successfully",
                fine = fine
            };
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }
}