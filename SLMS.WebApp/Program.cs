using SLMS.WebApp.Services;
using SLMS.WebApp.Services.Interfaces;
using SLMS.WebApp.Services.DigitalLibrary;

var builder = WebApplication.CreateBuilder(args);

// MVC
builder.Services.AddControllersWithViews();

// Common HttpClient
builder.Services.AddHttpClient();

// Existing Services
builder.Services.AddHttpClient<EmployeeService>(client =>
{
    client.BaseAddress =
        new Uri("https://localhost:7277/");
});

builder.Services.AddHttpClient<CustodyHistoryService>(client =>
{
    client.BaseAddress =
        new Uri("https://localhost:7277/");
});

builder.Services.AddHttpClient<AuditLogService>(client =>
{
    client.BaseAddress =
        new Uri("https://localhost:7277/");
});

// Catalog Services
builder.Services.AddHttpClient<CategoryService>();
builder.Services.AddHttpClient<LibraryResourceService>();
builder.Services.AddHttpClient<BookIssueService>();

// Inventory
builder.Services.AddScoped<
    IInventoryService,
    InventoryService>();

// Digital Library
builder.Services.AddScoped<
    IDigitalLibraryService,
    DigitalLibraryService>();

// Auth
builder.Services.AddHttpClient<AuthService>(client =>
{
    client.BaseAddress =
        new Uri("http://localhost:5062/");
});

// Session
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout =
        TimeSpan.FromMinutes(30);

    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

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
    pattern: "{controller=Auth}/{action=Login}/{id?}");

app.Run();
