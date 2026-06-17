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

#region Repository Registration

// Department
builder.Services.AddScoped<
    IDepartmentRepository,
    DepartmentRepository>();

// Digital Library
builder.Services.AddScoped<
    IDigitalContentRepository,
    DigitalContentRepository>();

builder.Services.AddScoped<
    IDigitalContentRequestRepository,
    DigitalContentRequestRepository>();

builder.Services.AddScoped<
    IPolicyRepository,
    PolicyRepository>();

builder.Services.AddScoped<
    IDownloadHistoryRepository,
    DownloadHistoryRepository>();

#endregion

#region Service Registration

// Department
builder.Services.AddScoped<
    IDepartmentService,
    DepartmentService>();

// Digital Library
builder.Services.AddScoped<
    IDigitalContentService,
    DigitalContentService>();

builder.Services.AddScoped<
    IDigitalContentRequestService,
    DigitalContentRequestService>();

builder.Services.AddScoped<
    IPolicyService,
    PolicyService>();

builder.Services.AddScoped<
    IDownloadHistoryService,
    DownloadHistoryService>();

#endregion

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