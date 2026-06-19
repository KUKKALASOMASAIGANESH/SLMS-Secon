namespace SLMS.WebApp.ViewModels;

public class TransactionDashboardViewModel
{
    public int TotalIssuedBooks { get; set; }

    public int TotalReturnedBooks { get; set; }

    public int TotalOverdueBooks { get; set; }

    public decimal TotalFineCollected { get; set; }
}