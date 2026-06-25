using SLMS.WebApp.Services;
using SLMS.WebApp.Services.Interfaces;
using SLMS.WebApp.Services.DigitalLibrary;
using SLMS.WebApp.Services.Transaction;
using SLMS.WebApp.Services.Transaction.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using SLMS.WebApp.Handlers;

var builder = WebApplication.CreateBuilder(args);

// Authentication
builder.Services.AddAuthentication(
    CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Auth/Login";
        options.AccessDeniedPath = "/Auth/AccessDenied";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
        options.SlidingExpiration = true;
    });

builder.Services.AddAuthorization();

// MVC
builder.Services.AddControllersWithViews();

// Session
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// JWT Handler
builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<JwtDelegatingHandler>();

//shelf
builder.Services.AddScoped<
    IShelfService,
    ShelfService>();

#region API Base URL

var apiBaseUrl = new Uri("http://localhost:5062/");

#endregion

#region Existing Services

builder.Services.AddHttpClient<EmployeeService>(client =>
{
    client.BaseAddress = apiBaseUrl;
})
.AddHttpMessageHandler<JwtDelegatingHandler>();

builder.Services.AddHttpClient<CustodyHistoryService>(client =>
{
    client.BaseAddress = apiBaseUrl;
})
.AddHttpMessageHandler<JwtDelegatingHandler>();

builder.Services.AddHttpClient<AuditLogService>(client =>
{
    client.BaseAddress = apiBaseUrl;
})
.AddHttpMessageHandler<JwtDelegatingHandler>();

builder.Services.AddHttpClient<DepartmentService>(client =>
{
    client.BaseAddress = apiBaseUrl;
})
.AddHttpMessageHandler<JwtDelegatingHandler>();

// AuthService should NOT use JwtDelegatingHandler
builder.Services.AddHttpClient<AuthService>(client =>
{
    client.BaseAddress = apiBaseUrl;
});

#endregion

#region Catalog Services

builder.Services.AddHttpClient<CategoryService>(client =>
{
    client.BaseAddress = apiBaseUrl;
})
.AddHttpMessageHandler<JwtDelegatingHandler>();

builder.Services.AddHttpClient<LibraryResourceService>(client =>
{
    client.BaseAddress = apiBaseUrl;
})
.AddHttpMessageHandler<JwtDelegatingHandler>();

builder.Services.AddHttpClient<BookIssueService>(client =>
{
    client.BaseAddress = apiBaseUrl;
})
.AddHttpMessageHandler<JwtDelegatingHandler>();

#endregion

#region Inventory

builder.Services.AddScoped<IInventoryService, InventoryService>();

#endregion



#region Digital Library
builder.Services.AddHttpClient<IDigitalLibraryService, DigitalLibraryService>(client =>
{
    client.BaseAddress = apiBaseUrl;
})
.AddHttpMessageHandler<JwtDelegatingHandler>();
#endregion

#region Transaction Dashboard

builder.Services.AddHttpClient<ITransactionDashboardService, TransactionDashboardService>(client =>
{
    client.BaseAddress = apiBaseUrl;
})
.AddHttpMessageHandler<JwtDelegatingHandler>();

#endregion

#region User Management

builder.Services.AddHttpClient<UserManagementService>(client =>
{
    client.BaseAddress = apiBaseUrl;
})
.AddHttpMessageHandler<JwtDelegatingHandler>();
builder.Services.AddHttpClient<DashboardService>(client =>
{
    client.BaseAddress = apiBaseUrl;
})
.AddHttpMessageHandler<JwtDelegatingHandler>();

#endregion

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=Login}/{id?}");

app.Run();