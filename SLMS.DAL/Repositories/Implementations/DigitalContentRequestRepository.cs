using Microsoft.EntityFrameworkCore;
using SLMS.DAL.Data;
using SLMS.DAL.Repositories.Interfaces;
using SLMS.DOL.Entities;

namespace SLMS.DAL.Repositories.Implementations;

public class DigitalContentRequestRepository
    : Repository<DigitalContentRequest>,
      IDigitalContentRequestRepository
{
    public DigitalContentRequestRepository(
        SLMSDbContext context)
        : base(context)
    {
    }

    public async Task<IEnumerable<DigitalContentRequest>>
    GetAllWithDetailsAsync()
    {
        return await _context.DigitalContentRequests
            .Include(x => x.Employee)
            .Include(x => x.DigitalContent)
            .ToListAsync();
    }

    public async Task<IEnumerable<DigitalContentRequest>>
        GetByEmployeeIdAsync(int employeeId)
    {
        return await _context.DigitalContentRequests
            .Where(x => x.EmployeeId == employeeId)
            .Include(x => x.Employee)
            .Include(x => x.DigitalContent)
            .ToListAsync();
    }
}