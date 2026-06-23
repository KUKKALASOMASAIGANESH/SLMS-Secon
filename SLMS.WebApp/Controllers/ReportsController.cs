using Microsoft.AspNetCore.Mvc;
using SLMS.WebApp.Models;   // ✅ IMPORTANT

namespace SLMS.WebApp.Controllers
{
    public class ReportsController : Controller
    {
        public async Task<ActionResult> Index()
        {
            var client = new HttpClient();

            var inventory = await client.GetFromJsonAsync<List<InventoryReport>>(
                "https://localhost:7277/api/reports/inventory");

            var issues = await client.GetFromJsonAsync<List<IssueReport>>(
                "https://localhost:7277/api/reports/issues");

            var overdue = await client.GetFromJsonAsync<List<OverdueReport>>(
                "https://localhost:7277/api/reports/overdue");

            var fine = await client.GetFromJsonAsync<List<FineReport>>(
                "https://localhost:7277/api/reports/fine");

            ViewBag.Inventory = inventory;
            ViewBag.Issues = issues;
            ViewBag.Overdue = overdue;
            ViewBag.Fine = fine;

            return View();
        }
    }
}