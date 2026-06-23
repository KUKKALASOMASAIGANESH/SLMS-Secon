using SLMS.BLL.Interfaces;
using SLMS.DAL.Repositories.Interfaces;
using SLMS.DOL.Entities;
using SLMS.Shared.DTOs.Custody;

namespace SLMS.BLL.Services;

public class CustodyHistoryService : ICustodyHistoryService
{
    private readonly ICustodyHistoryRepository _repository;

    public CustodyHistoryService(
        ICustodyHistoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<CustodyHistory>>
        GetAllAsync()
    {
        return await _repository
            .GetAllWithDetailsAsync();
    }

    public async Task<IEnumerable<CustodyHistory>>
        GetByInventoryItemAsync(int inventoryItemId)
    {
        return await _repository
            .GetByInventoryItemAsync(inventoryItemId);
    }

    public async Task<CustodyHistory?>
        GetByIdAsync(int id)
    {
        return await _repository
            .GetByIdAsync(id);
    }

    public async Task AddAsync(
        CustodyHistory custodyHistory)
    {
        await _repository
            .AddAsync(custodyHistory);

        await _repository
            .SaveChangesAsync();
    }

    public async Task<CustodyHistory?>
        GetCurrentCustodianAsync(
            int inventoryItemId)
    {
        return await _repository
            .GetCurrentCustodianAsync(inventoryItemId);
    }

    public async Task<IEnumerable<CustodyHistoryReportDto>>
        GetCustodyReportAsync()
    {
        return await _repository
            .GetCustodyReportAsync();
    }
}