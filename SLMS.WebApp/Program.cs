using SLMS.WebApp.Services;
<<<<<<< HEAD
=======
using SLMS.WebApp.Services.Interfaces;
>>>>>>> feature-inventory

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
<<<<<<< HEAD
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
=======

builder.Services.AddHttpClient();

builder.Services.AddScoped<
    IInventoryService,
    InventoryService>();

>>>>>>> feature-inventory
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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
