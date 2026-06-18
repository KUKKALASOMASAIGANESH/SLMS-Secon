using SLMS.WebApp.Services;
using SLMS.WebApp.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient();

builder.Services.AddHttpClient<EmployeeService>(
    client =>
    {
        client.BaseAddress =
            new Uri("https://localhost:7277/");
    });

builder.Services.AddHttpClient<CustodyHistoryService>(
    client =>
    {
        client.BaseAddress =
            new Uri("https://localhost:7277/");
    });

builder.Services.AddHttpClient<AuditLogService>(
    client =>
    {
        client.BaseAddress =
            new Uri("https://localhost:7277/");
    });

builder.Services.AddScoped<
    IInventoryService,
    InventoryService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();