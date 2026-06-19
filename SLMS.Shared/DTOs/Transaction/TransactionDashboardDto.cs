namespace SLMS.Shared.DTOs.Transaction;

public class TransactionDashboardDto
{
    public int TotalIssuedBooks { get; set; }

    public int TotalReturnedBooks { get; set; }

    public int OverdueBooks { get; set; }

    public decimal TotalFineCollected { get; set; }
}