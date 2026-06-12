using AutoMapper;
using SLMS.BLL.Interfaces;
using SLMS.DAL.Repositories.Interfaces;
using SLMS.DOL.Entities;
using SLMS.Shared.DTOs.Inventory;

namespace SLMS.BLL.Services;

public class InventoryService : IInventoryService
{
    private readonly IInventoryRepository _repository;
    private readonly IMapper _mapper;

    public InventoryService(
        IInventoryRepository repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<InventoryItemDto>> GetAllAsync()
    {
       var inventoryItems = await _repository.GetAllAsync();
    
       return _mapper.Map<IEnumerable<InventoryItemDto>>(inventoryItems);
   }
  



    public async Task<InventoryItemDto?> GetByIdAsync(int id)
    {
        var inventoryItem = await _repository.GetByIdAsync(id);

        if (inventoryItem == null)
            return null;

        return _mapper.Map<InventoryItemDto>(inventoryItem);
    }

    public async Task CreateAsync(CreateInventoryItemDto dto)
    {
        var inventoryItem = _mapper.Map<InventoryItem>(dto);

        await _repository.AddAsync(inventoryItem);

        await _repository.SaveChangesAsync();
    }


    public async Task<bool> UpdateAsync(
    int id,
    UpdateInventoryItemDto dto)
    {
        var inventoryItem =
            await _repository.GetByIdAsync(id);

        if (inventoryItem == null)
            return false;

        inventoryItem.AccessionNumber =
            dto.AccessionNumber;

        inventoryItem.InventoryNumber =
            dto.InventoryNumber;

        inventoryItem.ShelfNumber =
            dto.ShelfNumber;

        inventoryItem.Price =
            dto.Price;

        _repository.Update(inventoryItem);

        await _repository.SaveChangesAsync();

        return true;
    }


    public async Task<bool> DeleteAsync(int id)
    {
        var inventoryItem =
            await _repository.GetByIdAsync(id);

        if (inventoryItem == null)
            return false;

        _repository.Delete(inventoryItem);

        await _repository.SaveChangesAsync();

        return true;
    }
}