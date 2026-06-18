public class RequestCreateDTO
{
    public int EmployeeId { get; set; }

    public int ResourceId { get; set; }  // ✅ NOT BookId

    public DateTime IssueDate { get; set; }

    public DateTime DueDate { get; set; }
}