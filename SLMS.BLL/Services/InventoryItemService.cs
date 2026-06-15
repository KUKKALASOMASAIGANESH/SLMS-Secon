using AutoMapper;

using SLMS.BLL.Interfaces;
using SLMS.DAL.Repositories.Interfaces;
using SLMS.DOL.Entities;
using SLMS.Shared.DTOs.InventoryItem;

namespace SLMS.BLL.Services;

public class InventoryItemService
    : IInventoryItemService
{
    private readonly IInventoryItemRepository
        _repository;

    private readonly IMapper _mapper;

    public InventoryItemService(
        IInventoryItemRepository repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<
        IEnumerable<InventoryItemResponseDto>>
        GetAllAsync()
    {
        var entities =
            await _repository.GetAllAsync();

        return _mapper.Map<
            IEnumerable<InventoryItemResponseDto>>
            (entities);
    }

    public async Task<
        InventoryItemResponseDto?>
        GetByIdAsync(int id)
    {
        var entity =
            await _repository.GetByIdAsync(id);

        if (entity == null)
            return null;

        return _mapper.Map<
            InventoryItemResponseDto>
            (entity);
    }

    public async Task<
        InventoryItemResponseDto>
        CreateAsync(
            InventoryItemCreateDto dto)
    {
        var entity =
            _mapper.Map<InventoryItem>(dto);

        await _repository.AddAsync(entity);

        await _repository.SaveChangesAsync();

        return _mapper.Map<
            InventoryItemResponseDto>
            (entity);
    }

    public async Task<
        InventoryItemResponseDto?>
        UpdateAsync(
            int id,
            InventoryItemUpdateDto dto)
    {
        var entity =
            await _repository.GetByIdAsync(id);

        if (entity == null)
            return null;

        entity.ResourceId = dto.ResourceId;
        entity.AccessionNumber = dto.AccessionNumber;
        entity.InventoryNumber = dto.InventoryNumber;
        entity.ShelfNumber = dto.ShelfNumber;
        entity.Price = dto.Price;

        _repository.Update(entity);

        await _repository.SaveChangesAsync();

        return _mapper.Map<
            InventoryItemResponseDto>
            (entity);
    }

    public async Task<bool>
        DeleteAsync(int id)
    {
        var entity =
            await _repository.GetByIdAsync(id);

        if (entity == null)
            return false;

        _repository.Delete(entity);

        await _repository.SaveChangesAsync();

        return true;
    }
}