using SLMS.WebApp.ViewModels;

namespace SLMS.WebApp.Services.Transaction.Interfaces;

public interface ITransactionDashboardService
{
    Task<TransactionDashboardViewModel>
        GetDashboardAsync();
}