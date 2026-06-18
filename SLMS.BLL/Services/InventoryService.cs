/*
 * InventoryService
 *
 * Purpose:
 * Implements business logic for the Inventory module.
 *
 * Responsibilities:
 * - Inventory item management
 * - Business rule validation
 * - Inventory search and filtering
 * - Pagination and sorting
 * - Shelf management
 * - Availability tracking
 * - Inventory reporting
 * - Soft delete and restore operations
 *
 * Flow:
 * Controller
 *    ↓
 * InventoryService
 *    ↓
 * InventoryRepository
 *    ↓
 * Database
 *
 * This layer validates business rules before interacting
 * with the repository layer.
 */

using AutoMapper;
using SLMS.BLL.Interfaces;
using SLMS.DAL.Repositories.Interfaces;
using SLMS.DOL.Entities;
using SLMS.Shared.DTOs.Common;
using SLMS.Shared.DTOs.Inventory;

namespace SLMS.BLL.Services;

// Implements inventory business logic including validation,
// search, reporting, availability tracking and inventory lifecycle management.
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

    // Retrieves all active inventory items and excludes soft deleted records.
    public async Task<IEnumerable<InventoryItemDto>> GetAllAsync()
    {
        var inventoryItems =
            (await _repository.GetAllAsync())
            .Where(x => x.IsActive);

        return _mapper.Map<IEnumerable<InventoryItemDto>>(
            inventoryItems);
    }



    // Retrieves a specific inventory item if it is active.
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

    // Searches inventory items using the supplied filter criteria.
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


    // Retrieves all inventory items assigned to a specific shelf.
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

    // Generates a shelf-wise summary report showing inventory distribution.
    public async Task<IEnumerable<ShelfSummaryDto>>
    GetShelfSummaryAsync()
    {
        return await _repository
            .GetShelfSummaryAsync();
    }

    // Creates a new inventory item after validating business rules.
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

    // Updates inventory details for an existing active inventory item.
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

    // Performs a soft delete by marking the inventory item as inactive.
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


    // Generates a summary report containing overall inventory statistics.
    public async Task<InventorySummaryDto>
    GetInventorySummaryAsync()
    {
        return await _repository
            .GetInventorySummaryAsync();
    }

    // Generates a resource-wise inventory report showing copy distribution.
    public async Task<IEnumerable<ResourceInventoryReportDto>>
    GetResourceInventoryReportAsync()
    {
        return await _repository
            .GetResourceInventoryReportAsync();
    }

    // Generates an inventory cost analysis report.
    public async Task<InventoryCostReportDto>
    GetInventoryCostReportAsync()
    {
        return await _repository
            .GetInventoryCostReportAsync();
    }

    // Returns inventory records using pagination.
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

    // Returns inventory records sorted by the requested field.
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


    // Retrieves inventory items that are currently available for issue.
    public async Task<IEnumerable<InventoryItemDto>>
    GetAvailableBooksAsync()
    {
        var books =
            await _repository.GetAvailableBooksAsync();

        return _mapper.Map<
            IEnumerable<InventoryItemDto>>(books);
    }

    // Retrieves inventory items that are currently unavailable.
    public async Task<IEnumerable<InventoryItemDto>>
    GetUnavailableBooksAsync()
    {
        var books =
            await _repository.GetUnavailableBooksAsync();

        return _mapper.Map<
            IEnumerable<InventoryItemDto>>(books);
    }

    // Generates a summary report of available and unavailable inventory items.
    public async Task<AvailabilityReportDto>
    GetAvailabilityReportAsync()
    {
        return await _repository
            .GetAvailabilityReportAsync();
    }


    // Restores a previously soft deleted inventory item.
    public async Task<bool> RestoreAsync(int id)
    {
        var inventoryItem =
            await _repository.GetByIdAsync(id);

        if (inventoryItem == null)
            return false;

        inventoryItem.IsActive = true;

        _repository.Update(inventoryItem);

        await _repository.SaveChangesAsync();

        return true;
    }


    // Retrieves all inactive (soft deleted) inventory items.
    public async Task<IEnumerable<InventoryItemDto>>
    GetInactiveAsync()
    {
        var inventoryItems =
            (await _repository.GetAllAsync())
            .Where(x => !x.IsActive);

        return _mapper.Map<
            IEnumerable<InventoryItemDto>>(
                inventoryItems);
    }
}