using Microsoft.AspNetCore.Mvc;
using SLMS.WebApp.Models.DigitalLibrary;
using SLMS.WebApp.Services.DigitalLibrary;

public class DigitalLibraryController : Controller
{
    private readonly IDigitalLibraryService _service;

    public DigitalLibraryController(
        IDigitalLibraryService service)
    {
        _service = service;
    }

    public async Task<IActionResult> Index()
    {
        var contents =
            await _service.GetContentsAsync();

        return View(contents);
    }

    public IActionResult Request(int id)
    {
        var model =
            new DigitalContentRequestViewModel
            {
                DigitalContentId = id
            };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Request(
    DigitalContentRequestViewModel model)
    {
        await _service.SubmitRequestAsync(model);

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Policies()
    {
        var policies =
            await _service.GetPoliciesAsync();

        return View(policies);
    }

    public async Task<IActionResult> Downloads()
    {
        var downloads =
            await _service
                .GetDownloadHistoryAsync();

        return View(downloads);
    }
}