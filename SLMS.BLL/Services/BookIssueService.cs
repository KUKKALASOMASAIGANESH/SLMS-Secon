using AutoMapper;

using SLMS.BLL.Interfaces;

using SLMS.DAL.Repositories.Interfaces;

using SLMS.DOL.Entities;

using SLMS.Shared.DTOs.BookIssue;

namespace SLMS.BLL.Services;

public class BookIssueService
    : IBookIssueService
{
    private readonly IBookIssueRepository _repository;

    private readonly IMapper _mapper;

    public BookIssueService(
        IBookIssueRepository repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<BookIssueResponseDto>>
        GetAllAsync()
    {
        var entities =
            await _repository.GetAllAsync();

        return _mapper.Map<
            IEnumerable<BookIssueResponseDto>>
            (entities);
    }

    public async Task<BookIssueResponseDto?>
        GetByIdAsync(int id)
    {
        var entity =
            await _repository.GetByIdAsync(id);

        if (entity == null)
            return null;

        return _mapper.Map<
            BookIssueResponseDto>(entity);
    }

    public async Task<BookIssueResponseDto>
        CreateAsync(BookIssueCreateDto dto)
    {
        var entity =
            _mapper.Map<BookIssue>(dto);

        await _repository.AddAsync(entity);

        await _repository.SaveChangesAsync();

        return _mapper.Map<
            BookIssueResponseDto>(entity);
    }

    public async Task<BookIssueResponseDto?>
        UpdateAsync(
            int id,
            BookIssueUpdateDto dto)
    {
        var entity =
            await _repository.GetByIdAsync(id);

        if (entity == null)
            return null;

        entity.InventoryItemId =
            dto.InventoryItemId;

        entity.EmployeeId =
            dto.EmployeeId;

        entity.IssueDate =
            dto.IssueDate;

        entity.DueDate =
            dto.DueDate;

        entity.IssuedByUserId =
            dto.IssuedByUserId;

        entity.Status =
            dto.Status;

        _repository.Update(entity);

        await _repository.SaveChangesAsync();

        return _mapper.Map<
            BookIssueResponseDto>(entity);
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