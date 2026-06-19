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

    
    public async Task<IActionResult> ManageContent()
    {
        var data =
            await _service.GetContentsAsync();

        return View(data);
    }

    public IActionResult CreateContent()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateContent(
    AdminDigitalContentViewModel model)
    {
        await _service.CreateContentAsync(model);

        return RedirectToAction(nameof(ManageContent));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var content =
            await _service.GetContentByIdAsync(id);

        if (content == null)
            return NotFound();

        var model =
            new AdminDigitalContentViewModel
            {
                Id = content.Id,
                Title = content.Title,
                ContentType = content.Category,
                FilePath = content.FilePath,
                Description = content.Description
            };

        return View("EditContent", model);
    }

    [HttpPost]
    public async Task<IActionResult> EditContent(
    AdminDigitalContentViewModel model)
    {
        await _service.UpdateContentAsync(model);

        return RedirectToAction(nameof(ManageContent));
    }

    public async Task<IActionResult> DeleteContent(int id)
    {
        await _service.DeleteContentAsync(id);

        return RedirectToAction(nameof(ManageContent));
    }

    public async Task<IActionResult> ManageRequests()
    {
        var requests =
            await _service.GetRequestsAsync();

        return View(requests);
    }

    public async Task<IActionResult> ApproveRequest(int id)
    {
        await _service.ApproveRequestAsync(id);

        return RedirectToAction(nameof(ManageRequests));
    }

    public async Task<IActionResult> RejectRequest(int id)
    {
        await _service.RejectRequestAsync(id);

        return RedirectToAction(nameof(ManageRequests));
    }
}