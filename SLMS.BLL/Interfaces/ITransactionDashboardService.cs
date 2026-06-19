using SLMS.Shared.DTOs.Transaction;

namespace SLMS.BLL.Interfaces;

public interface ITransactionDashboardService
{
    Task<TransactionDashboardDto>
        GetDashboardAsync();
}