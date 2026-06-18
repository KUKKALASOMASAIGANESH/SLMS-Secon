<<<<<<< HEAD
=======
﻿using Microsoft.EntityFrameworkCore;
>>>>>>> feature-custody
using SLMS.DAL.Data;
using SLMS.DAL.Repositories.Interfaces;
using SLMS.DOL.Entities;

namespace SLMS.DAL.Repositories.Implementations;

public class EmployeeRepository
<<<<<<< HEAD
	: Repository<Employee>, IEmployeeRepository
{
	public EmployeeRepository(
		SLMSDbContext context)
		: base(context)
	{
	}
=======
    : Repository<Employee>, IEmployeeRepository
{
    public EmployeeRepository(
        SLMSDbContext context)
        : base(context)
    {
    }
    public async Task<IEnumerable<Employee>>
    SearchByNameAsync(string name)
    {
        return await _dbSet
            .Where(x => x.FullName.Contains(name))
            .ToListAsync();
    }
>>>>>>> feature-custody
}