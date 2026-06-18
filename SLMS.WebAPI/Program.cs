using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.EntityFrameworkCore;

using SLMS.DAL.Data;
using SLMS.BLL.Interfaces;
using SLMS.BLL.Services;
using SLMS.BLL.Helpers;
using SLMS.DAL.Repositories.Interfaces;
using SLMS.DAL.Repositories.Implementations;
using Microsoft.OpenApi.Models;


using SLMS.WebAPI.Mappings;
using SLMS.WebAPI.Middleware;

var builder = WebApplication.CreateBuilder(args);

<<<<<<< HEAD
// ===================== SERVICES =====================

// Controllers + Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
=======
// Controllers
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Database
builder.Services.AddDbContext<SLMSDbContext>(options =>
>>>>>>> a184551 (Completed Department Module V2 with DTOs AutoMapper Middleware)
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "SLMS API",
        Version = "v1"
    });

    options.AddSecurityDefinition("Bearer",
        new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "Enter JWT Token"
        });

    options.AddSecurityRequirement(
        new OpenApiSecurityRequirement
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

<<<<<<< HEAD







// DB
builder.Services.AddDbContext<SLMSDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();

// Services
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
=======
// Repositories
builder.Services.AddScoped<
    IDepartmentRepository,
    DepartmentRepository>();

// Services
builder.Services.AddScoped<
    IDepartmentService,
    DepartmentService>();

// AutoMapper
builder.Services.AddAutoMapper(
    typeof(MappingProfile));
>>>>>>> a184551 (Completed Department Module V2 with DTOs AutoMapper Middleware)

// JWT Helper
builder.Services.AddScoped<JwtTokenHelper>();

// ===================== JWT AUTH =====================
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
    };
});

// ===================== APP BUILD =====================
var app = builder.Build();

<<<<<<< HEAD
// ===================== PIPELINE =====================
=======
// Swagger
>>>>>>> a184551 (Completed Department Module V2 with DTOs AutoMapper Middleware)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Global Exception Middleware
app.UseMiddleware<GlobalExceptionMiddleware>();

// HTTPS
app.UseHttpsRedirection();

<<<<<<< HEAD
app.UseAuthentication();   // MUST be before Authorization
=======
// Authorization
>>>>>>> a184551 (Completed Department Module V2 with DTOs AutoMapper Middleware)
app.UseAuthorization();

// Controllers
app.MapControllers();

app.Run();