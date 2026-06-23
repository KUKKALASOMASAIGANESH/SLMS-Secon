namespace SLMS.WebApp.Models
{
    public class InventoryReport
    {
        public string title { get; set; }
        public string category { get; set; }
        public int totalCount { get; set; }
    }

    public class IssueReport
    {
        public string bookTitle { get; set; }
        public string employeeName { get; set; }
        public DateTime issueDate { get; set; }
        public DateTime dueDate { get; set; }
    }

    public class OverdueReport
    {
        public string bookTitle { get; set; }
        public string employeeName { get; set; }
        public int daysOverdue { get; set; }
    }

    public class FineReport
    {
        public string employeeName { get; set; }
        public int fineAmount { get; set; }
    }
}
