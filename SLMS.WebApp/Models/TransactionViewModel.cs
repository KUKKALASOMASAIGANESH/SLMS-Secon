namespace SLMS.WebApp.Models
{
    public class TransactionViewModel
    {
        public int EmployeeId { get; set; }

        public int ResourceId { get; set; }

        public DateTime IssueDate { get; set; }

        public DateTime DueDate { get; set; }
    }
}