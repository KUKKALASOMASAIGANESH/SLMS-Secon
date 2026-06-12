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
builder.Services.AddScoped<
    ICustodyHistoryRepository,
    CustodyHistoryRepository>();
builder.Services.AddScoped<
    IEmployeeRepository,
    EmployeeRepository>();
// Service Registration
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<
    ICustodyHistoryService,
    CustodyHistoryService>();
builder.Services.AddScoped<
    IEmployeeService,
    EmployeeService>();
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