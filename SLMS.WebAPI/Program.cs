using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Security.Claims;

using SLMS.DAL.Data;
using SLMS.BLL.Interfaces;
using SLMS.BLL.Services;
using SLMS.BLL.Helpers;
using SLMS.DAL.Repositories.Interfaces;
using SLMS.DAL.Repositories.Implementations;
using SLMS.WebAPI.Mappings;
using SLMS.WebAPI.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "SLMS API",
        Version = "v1"
    });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter JWT token"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddDbContext<SLMSDbContext>(options =>
{
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection"));
});

#region Repositories

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<ICustodyHistoryRepository, CustodyHistoryRepository>();
builder.Services.AddScoped<IAuditLogRepository, AuditLogRepository>();

builder.Services.AddScoped<IInventoryRepository, InventoryRepository>();
builder.Services.AddScoped<IShelfRepository, ShelfRepository>();

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ILibraryResourceRepository, LibraryResourceRepository>();
builder.Services.AddScoped<IInventoryItemRepository, InventoryItemRepository>();

builder.Services.AddScoped<IBookIssueRepository, BookIssueRepository>();
builder.Services.AddScoped<IBookReturnRepository, BookReturnRepository>();

builder.Services.AddScoped<IRequestRepository, RequestRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IPermissionRepository, PermissionRepository>();
builder.Services.AddScoped<IRolePermissionRepository, RolePermissionRepository>();

builder.Services.AddScoped<IDigitalContentRepository, DigitalContentRepository>();
builder.Services.AddScoped<IDigitalContentRequestRepository, DigitalContentRequestRepository>();
builder.Services.AddScoped<IPolicyRepository, PolicyRepository>();
builder.Services.AddScoped<IDownloadHistoryRepository, DownloadHistoryRepository>();
builder.Services.AddScoped<IReportRepository, ReportRepository>();

builder.Services.AddScoped<IDashboardRepository, DashboardRepository>();
builder.Services.AddScoped<IDashboardRepository,
                           DashboardRepository>();

#endregion

#region Services

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<ICustodyHistoryService, CustodyHistoryService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IAuditLogService, AuditLogService>();

builder.Services.AddScoped<IInventoryService, InventoryService>();
builder.Services.AddScoped<IShelfService, ShelfService>();

builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ILibraryResourceService, LibraryResourceService>();
builder.Services.AddScoped<IInventoryItemService, InventoryItemService>();

builder.Services.AddScoped<IBookIssueService, BookIssueService>();
builder.Services.AddScoped<IBookReturnService, BookReturnService>();
builder.Services.AddScoped<ITransactionDashboardService, TransactionDashboardService>();

builder.Services.AddScoped<IRequestService, RequestService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IPermissionService, PermissionService>();
builder.Services.AddScoped<IRolePermissionService, RolePermissionService>();

builder.Services.AddScoped<IDigitalContentService, DigitalContentService>();
builder.Services.AddScoped<IDigitalContentRequestService, DigitalContentRequestService>();
builder.Services.AddScoped<IPolicyService, PolicyService>();
builder.Services.AddScoped<IDownloadHistoryService, DownloadHistoryService>();

builder.Services.AddScoped<ReportService>();
builder.Services.AddScoped<IDashboardService, DashboardService>();

#endregion

builder.Services.AddScoped<JwtTokenHelper>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters =
            new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,

                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidAudience = builder.Configuration["Jwt:Audience"],

                IssuerSigningKey =
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(
                            builder.Configuration["Jwt:Key"]!)),

                RoleClaimType = ClaimTypes.Role,
                NameClaimType = ClaimTypes.Name
            };
    });

builder.Services.AddAuthorization();
builder.Services.AddHttpClient<IAIService, AIService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();