using AutoMapper;

using SLMS.BLL.Interfaces;
using SLMS.DAL.Repositories.Interfaces;
using SLMS.DOL.Entities;
using SLMS.Shared.DTOs.BookReturn;

namespace SLMS.BLL.Services;

public class BookReturnService
    : IBookReturnService
{
    private readonly IBookReturnRepository _repository;
    private readonly IBookIssueRepository _bookIssueRepository;
    private readonly IMapper _mapper;

    public BookReturnService(
    IBookReturnRepository repository,
    IBookIssueRepository bookIssueRepository,
    IMapper mapper)
    {
        _repository = repository;
        _bookIssueRepository = bookIssueRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<BookReturnResponseDto>>
        GetAllAsync()
    {
        var entities =
            await _repository.GetAllAsync();

        return _mapper.Map<
            IEnumerable<BookReturnResponseDto>>
            (entities);
    }

    public async Task<BookReturnResponseDto?>
        GetByIdAsync(int id)
    {
        var entity =
            await _repository.GetByIdAsync(id);

        if (entity == null)
            return null;

        return _mapper.Map<
            BookReturnResponseDto>(entity);
    }

    public async Task<BookReturnResponseDto>
        CreateAsync(BookReturnCreateDto dto)
    {
        dto.ReturnDate =
            DateTime.SpecifyKind(
                dto.ReturnDate,
                DateTimeKind.Utc);


        var issue =
    await _bookIssueRepository
        .GetByIdAsync(dto.BookIssueId);

        if (issue == null)
        {
            throw new Exception(
                $"Book Issue not found. Received BookIssueId = {dto.BookIssueId}");
        }

        decimal fine = 0;

        /* var issue =
      await _bookIssueRepository
          .GetByIdAsync(dto.BookIssueId);

         decimal fine = 0;*/

        if (issue != null &&
            dto.ReturnDate.Date >
            issue.DueDate.Date)
        {
            int lateDays =
                (dto.ReturnDate.Date -
                 issue.DueDate.Date).Days;

            fine = lateDays * 10;
        }

        var entity =
            _mapper.Map<BookReturn>(dto);

        entity.FineAmount = fine;

        // Update Book Issue status
        issue.Status = "Returned";

        _bookIssueRepository.Update(issue);

        await _repository.AddAsync(entity);

        await _repository.SaveChangesAsync();

        return _mapper.Map<
            BookReturnResponseDto>(entity);
    }

    public async Task<BookReturnResponseDto?>
        UpdateAsync(
            int id,
            BookReturnUpdateDto dto)
    {
        var entity =
            await _repository.GetByIdAsync(id);

        if (entity == null)
            return null;

        entity.BookIssueId =
            dto.BookIssueId;

        entity.ReturnDate =
            DateTime.SpecifyKind(
                dto.ReturnDate,
                DateTimeKind.Utc);

        entity.Condition =
            dto.Condition;

        entity.Remarks =
            dto.Remarks;

        entity.FineAmount =
            dto.FineAmount;

        entity.ReturnedByUserId =
            dto.ReturnedByUserId;

        _repository.Update(entity);

        await _repository.SaveChangesAsync();

        return _mapper.Map<
            BookReturnResponseDto>(entity);
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