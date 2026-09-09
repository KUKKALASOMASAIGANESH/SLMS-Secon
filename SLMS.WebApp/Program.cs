using SLMS.WebApp.Services;
using SLMS.WebApp.Services.Interfaces;
using SLMS.WebApp.Services.DigitalLibrary;
using SLMS.WebApp.Services.Transaction;
using SLMS.WebApp.Services.Transaction.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using SLMS.WebApp.Handlers;

var builder = WebApplication.CreateBuilder(args);

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

builder.Services.AddControllersWithViews();

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<JwtDelegatingHandler>();

var apiBaseUrl = new Uri("http://localhost:5062/");

// Named client used by TransactionController
builder.Services.AddHttpClient("SLMSApi", client =>
{
    client.BaseAddress = apiBaseUrl;
})
.AddHttpMessageHandler<JwtDelegatingHandler>();

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

builder.Services.AddHttpClient<AuthService>(client =>
{
    client.BaseAddress = apiBaseUrl;
});

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

builder.Services.AddHttpClient<IInventoryService, InventoryService>(client =>
{
    client.BaseAddress = apiBaseUrl;
})
.AddHttpMessageHandler<JwtDelegatingHandler>();

builder.Services.AddHttpClient<IDigitalLibraryService, DigitalLibraryService>(client =>
{
    client.BaseAddress = apiBaseUrl;
})
.AddHttpMessageHandler<JwtDelegatingHandler>();

builder.Services.AddHttpClient<ITransactionDashboardService, TransactionDashboardService>(client =>
{
    client.BaseAddress = apiBaseUrl;
})
.AddHttpMessageHandler<JwtDelegatingHandler>();

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

builder.Services.AddHttpClient<IShelfService, ShelfService>(client =>
{
    client.BaseAddress = apiBaseUrl;
})
.AddHttpMessageHandler<JwtDelegatingHandler>();
builder.Services.AddHttpClient<AIChatService>(client =>
{
    client.BaseAddress = apiBaseUrl;
})
.AddHttpMessageHandler<JwtDelegatingHandler>();

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