using AutoMapper;
using SLMS.BLL.Interfaces;
using SLMS.DAL.Repositories.Interfaces;
using SLMS.DOL.Entities;
using SLMS.Shared.DTOs.Common;
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
        var inventoryItems =
            (await _repository.GetAllAsync())
            .Where(x => x.IsActive);

        return _mapper.Map<IEnumerable<InventoryItemDto>>(
            inventoryItems);
    }




    public async Task<InventoryItemDto?> GetByIdAsync(int id)
    {
        var inventoryItem = await _repository.GetByIdAsync(id);

        if (inventoryItem == null ||
    !inventoryItem.IsActive)
        {
            return null;
        }

        return _mapper.Map<InventoryItemDto>(inventoryItem);
    }

    public async Task<IEnumerable<InventoryItemDto>> SearchAsync(
     InventorySearchDto searchDto)
    {
        var inventoryItems =
            await _repository.SearchAsync(
                searchDto.AccessionNumber,
                searchDto.InventoryNumber,
                searchDto.ShelfNumber,
                searchDto.ResourceId,
                searchDto.Title,
                searchDto.Author,
                searchDto.Publisher,
                searchDto.MinPrice,
                searchDto.MaxPrice);

        inventoryItems =
    inventoryItems.Where(x => x.IsActive);

        return _mapper.Map<IEnumerable<InventoryItemDto>>(
            inventoryItems);
    }

    public async Task<IEnumerable<InventoryItemDto>>
    GetByShelfAsync(string shelfNumber)
    {
        var inventoryItems =
            await _repository.GetByShelfAsync(
                shelfNumber);

        return _mapper.Map<
            IEnumerable<InventoryItemDto>>(
                inventoryItems);
    }

    public async Task<IEnumerable<ShelfSummaryDto>>
    GetShelfSummaryAsync()
    {
        return await _repository
            .GetShelfSummaryAsync();
    }

    public async Task CreateAsync(CreateInventoryItemDto dto)
    {

        if (!await _repository.ResourceExistsAsync(dto.ResourceId))
        {
            throw new Exception("Resource does not exist.");
        }

        if (await _repository.AccessionNumberExistsAsync(
                dto.AccessionNumber))
        {
            throw new Exception(
                "Accession Number already exists.");
        }

        if (await _repository.InventoryNumberExistsAsync(
                dto.InventoryNumber))
        {
            throw new Exception(
                "Inventory Number already exists.");
        }

        if (dto.Price < 0)
        {
            throw new Exception(
                "Price cannot be negative.");
        }
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

        if (inventoryItem == null ||
    !inventoryItem.IsActive)
        {
            return false;
        }

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

        inventoryItem.IsActive = false;

        _repository.Update(inventoryItem);

        await _repository.SaveChangesAsync();

        return true;
    }

    public async Task<InventorySummaryDto>
    GetInventorySummaryAsync()
    {
        return await _repository
            .GetInventorySummaryAsync();
    }

    public async Task<IEnumerable<ResourceInventoryReportDto>>
    GetResourceInventoryReportAsync()
    {
        return await _repository
            .GetResourceInventoryReportAsync();
    }

    public async Task<InventoryCostReportDto>
    GetInventoryCostReportAsync()
    {
        return await _repository
            .GetInventoryCostReportAsync();
    }

    public async Task<PagedResultDto<InventoryItemDto>>
    GetPagedAsync(
        InventoryPaginationDto paginationDto)
    {
        var inventoryItems =
    (await _repository.GetAllAsync())
    .Where(x => x.IsActive);

        var totalCount =
            inventoryItems.Count();

        var pagedItems =
            inventoryItems
                .Skip(
                    (paginationDto.Page - 1)
                    * paginationDto.PageSize)
                .Take(
                    paginationDto.PageSize);

        return new PagedResultDto<InventoryItemDto>
        {
            Items =
                _mapper.Map<
                    IEnumerable<InventoryItemDto>>(
                        pagedItems),

            TotalCount = totalCount,

            Page = paginationDto.Page,

            PageSize = paginationDto.PageSize
        };
    }

    public async Task<IEnumerable<InventoryItemDto>>
    GetSortedAsync(
        InventorySortDto sortDto)
    {
        var inventoryItems =
    (await _repository.GetAllAsync())
    .Where(x => x.IsActive);

        if (!string.IsNullOrWhiteSpace(
                sortDto.SortBy))
        {
            switch (sortDto.SortBy.ToLower())
            {
                case "price":

                    inventoryItems =
                        sortDto.Descending
                        ? inventoryItems.OrderByDescending(
                            x => x.Price)
                        : inventoryItems.OrderBy(
                            x => x.Price);

                    break;

                case "accessionnumber":

                    inventoryItems =
                        sortDto.Descending
                        ? inventoryItems.OrderByDescending(
                            x => x.AccessionNumber)
                        : inventoryItems.OrderBy(
                            x => x.AccessionNumber);

                    break;

                case "shelfnumber":

                    inventoryItems =
                        sortDto.Descending
                        ? inventoryItems.OrderByDescending(
                            x => x.ShelfNumber)
                        : inventoryItems.OrderBy(
                            x => x.ShelfNumber);

                    break;
            }
        }

        return _mapper.Map<
            IEnumerable<InventoryItemDto>>(
                inventoryItems);
    }
}