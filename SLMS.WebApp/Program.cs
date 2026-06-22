<<<<<<< HEAD
using SLMS.WebApp.Services;
using SLMS.WebApp.Services.Interfaces;
using SLMS.WebApp.Services.DigitalLibrary;
using SLMS.WebApp.Services.Transaction;
using SLMS.WebApp.Services.Transaction.Interfaces;
=======
using SLMS.WebApp.Services.DigitalLibrary;

>>>>>>> feature-digital-library

var builder = WebApplication.CreateBuilder(args);

// MVC
builder.Services.AddControllersWithViews();

<<<<<<< HEAD
// Common HttpClient
builder.Services.AddHttpClient();

#region API Base URL

var apiBaseUrl =
    new Uri("http://localhost:5062/");

#endregion

#region Existing Services

builder.Services.AddHttpClient<EmployeeService>(
    client =>
    {
        client.BaseAddress = apiBaseUrl;
    });

builder.Services.AddHttpClient<CustodyHistoryService>(
    client =>
    {
        client.BaseAddress = apiBaseUrl;
    });

builder.Services.AddHttpClient<AuditLogService>(
    client =>
    {
        client.BaseAddress = apiBaseUrl;
    });

builder.Services.AddHttpClient<AuthService>(
    client =>
    {
        client.BaseAddress = apiBaseUrl;
    });

builder.Services.AddHttpClient<DepartmentService>(
    client =>
    {
        client.BaseAddress = apiBaseUrl;
    });

#endregion

#region Catalog Services

builder.Services.AddHttpClient<CategoryService>(
    client =>
    {
        client.BaseAddress = apiBaseUrl;
    });

builder.Services.AddHttpClient<LibraryResourceService>(
    client =>
    {
        client.BaseAddress = apiBaseUrl;
    });

builder.Services.AddHttpClient<BookIssueService>(
    client =>
    {
        client.BaseAddress = apiBaseUrl;
    });

#endregion

#region Inventory

builder.Services.AddScoped<
    IInventoryService,
    InventoryService>();

#endregion

#region Digital Library

=======
builder.Services.AddHttpClient();

>>>>>>> feature-digital-library
builder.Services.AddScoped<
    IDigitalLibraryService,
    DigitalLibraryService>();

<<<<<<< HEAD
#endregion

#region Transaction Dashboard

builder.Services.AddHttpClient<
    ITransactionDashboardService,
    TransactionDashboardService>(
    client =>
    {
        client.BaseAddress = apiBaseUrl;
    });

#endregion

#region Session

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout =
        TimeSpan.FromMinutes(30);

    options.Cookie.HttpOnly = true;

    options.Cookie.IsEssential = true;
});

#endregion

=======
>>>>>>> feature-digital-library
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

// app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern:
    "{controller=Auth}/{action=Login}/{id?}");

app.Run();