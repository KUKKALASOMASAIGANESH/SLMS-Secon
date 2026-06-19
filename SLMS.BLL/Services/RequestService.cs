using AutoMapper;

using SLMS.BLL.Interfaces;
using SLMS.DAL.Repositories.Interfaces;
using SLMS.DOL.Entities;
using SLMS.Shared.DTOs.Request;

namespace SLMS.BLL.Services;

public class RequestService
    : IRequestService
{
    private readonly IRequestRepository
        _repository;

    private readonly IMapper _mapper;

    public RequestService(
        IRequestRepository repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<
        IEnumerable<RequestResponseDto>>
        GetAllAsync()
    {
        var entities =
            await _repository.GetAllAsync();

        return _mapper.Map<
            IEnumerable<RequestResponseDto>>
            (entities);
    }

    public async Task<
        RequestResponseDto?>
        GetByIdAsync(int id)
    {
        var entity =
            await _repository.GetByIdAsync(id);

        if (entity == null)
            return null;

        return _mapper.Map<
            RequestResponseDto>
            (entity);
    }

    public async Task<
        RequestResponseDto>
        CreateAsync(
            RequestCreateDto dto)
    {
        var entity =
            _mapper.Map<Request>(dto);

        await _repository.AddAsync(entity);

        await _repository.SaveChangesAsync();

        return _mapper.Map<
            RequestResponseDto>
            (entity);
    }

    public async Task<
        RequestResponseDto?>
        UpdateAsync(
            int id,
            RequestUpdateDto dto)
    {
        var entity =
            await _repository.GetByIdAsync(id);

        if (entity == null)
            return null;

        entity.EmployeeId = dto.EmployeeId;
        entity.ResourceId = dto.ResourceId;
        entity.RequestType = dto.RequestType;
        entity.Status = dto.Status;
        entity.RequestDate = dto.RequestDate;
        entity.Remarks = dto.Remarks;

        _repository.Update(entity);

        await _repository.SaveChangesAsync();

        return _mapper.Map<
            RequestResponseDto>
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
    public async Task<RequestResponseDto?>
    ApproveAsync(int id)
    {
        var entity =
            await _repository.GetByIdAsync(id);

        if (entity == null)
            return null;

        entity.Status = "Approved";

        _repository.Update(entity);

        await _repository.SaveChangesAsync();

        return _mapper.Map<RequestResponseDto>(entity);
    }

    public async Task<RequestResponseDto?>
        RejectAsync(int id)
    {
        var entity =
            await _repository.GetByIdAsync(id);

        if (entity == null)
            return null;

        entity.Status = "Rejected";

        _repository.Update(entity);

        await _repository.SaveChangesAsync();

        return _mapper.Map<RequestResponseDto>(entity);
    }
}