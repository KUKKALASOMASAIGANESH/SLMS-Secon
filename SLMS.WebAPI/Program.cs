using SLMS.WebAPI.Mappings;

using Microsoft.EntityFrameworkCore;

using SLMS.DAL.Data;

using SLMS.BLL.Interfaces;
using SLMS.BLL.Services;

using SLMS.DAL.Repositories.Interfaces;
using SLMS.DAL.Repositories.Implementations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

// Database

builder.Services.AddDbContext<SLMSDbContext>(options =>
{
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Repository Registration

builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

builder.Services.AddScoped<ILibraryResourceRepository, LibraryResourceRepository>();

builder.Services.AddScoped<IInventoryItemRepository, InventoryItemRepository>();

builder.Services.AddScoped<IRequestRepository, RequestRepository>();

builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();

builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IBookIssueRepository, BookIssueRepository>();

builder.Services.AddScoped<IRoleRepository, RoleRepository>();

builder.Services.AddScoped<IPermissionRepository, PermissionRepository>();

builder.Services.AddScoped<IRolePermissionRepository, RolePermissionRepository>();

// Service Registration

builder.Services.AddScoped<IDepartmentService, DepartmentService>();

builder.Services.AddScoped<ICategoryService, CategoryService>();

builder.Services.AddScoped<ILibraryResourceService, LibraryResourceService>();

builder.Services.AddScoped<IInventoryItemService, InventoryItemService>();

builder.Services.AddScoped<IRequestService, RequestService>();

builder.Services.AddScoped<IEmployeeService, EmployeeService>();

builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IBookIssueService, BookIssueService>();

builder.Services.AddScoped<IRoleService, RoleService>();

builder.Services.AddScoped<IPermissionService, PermissionService>();

builder.Services.AddScoped<IRolePermissionService, RolePermissionService>();

// AutoMapper

builder.Services.AddAutoMapper(
    typeof(MappingProfile));

var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();