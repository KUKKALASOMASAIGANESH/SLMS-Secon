using Microsoft.EntityFrameworkCore;
using SLMS.DAL.Data;

using SLMS.BLL.Interfaces;
using SLMS.BLL.Services;

using SLMS.DAL.Repositories.Interfaces;
using SLMS.DAL.Repositories.Implementations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<TransactionService>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IRequestService, RequestService>();
// Database
builder.Services.AddDbContext<SLMSDbContext>(options =>
{
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Repository Registration
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();

// Service Registration
builder.Services.AddScoped<IDepartmentService, DepartmentService>();

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