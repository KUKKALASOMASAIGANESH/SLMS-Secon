using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SLMS.WebApp.Models.DigitalLibrary;
using SLMS.WebApp.Services.DigitalLibrary;
[Authorize(Roles = "Admin,Librarian,User")]
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

        var requests =
            await _service.GetMyRequestsAsync(1);

        ViewBag.Requests = requests;

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
        Console.WriteLine(
            $"POST REQUEST -> ContentId={model.DigitalContentId}");

        Console.WriteLine(
            $"POST REQUEST -> Reason={model.Reason}");

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
        if (!ModelState.IsValid)
        {
            return View(model);
        }
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

    public async Task<IActionResult>
    ManagePolicies()
    {
        var data =
            await _service
            .GetPoliciesForAdminAsync();

        return View(data);
    }

    public IActionResult CreatePolicy()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult>
    CreatePolicy(
    AdminPolicyViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        await _service
            .CreatePolicyAsync(model);

        return RedirectToAction(
            nameof(ManagePolicies));
    }

    public async Task<IActionResult>
    EditPolicy(int id)
    {
        var model =
            await _service
            .GetPolicyByIdAsync(id);

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult>
    EditPolicy(
    AdminPolicyViewModel model)
    {
        await _service
            .UpdatePolicyAsync(model);

        return RedirectToAction(
            nameof(ManagePolicies));
    }

    public async Task<IActionResult>
    DeletePolicy(int id)
    {
        await _service
            .DeletePolicyAsync(id);

        return RedirectToAction(
            nameof(ManagePolicies));
    }

    

    public async Task<IActionResult> Read(int id)
    {
        var content =
            await _service.GetContentByIdAsync(id);

        if (content == null)
            return NotFound();

        return View(content);
    }

    public async Task<IActionResult> Download(int id)
    {
        int employeeId = 1;

        bool canAccess =
            await _service.CanAccessContentAsync(
                id,
                employeeId);

        if (!canAccess)
        {
            TempData["Error"] =
                "Your request is not approved yet.";

            return RedirectToAction(nameof(Index));
        }

        await _service.AddDownloadHistoryAsync(
            employeeId,
            id);

        var content =
            await _service.GetContentByIdAsync(id);

        if (content == null)
            return NotFound();

        using var client = new HttpClient();

        var fileBytes =
            await client.GetByteArrayAsync(content.FilePath);

        return File(
            fileBytes,
            "application/pdf",
            $"{content.Title}.pdf");
    }

    public async Task<IActionResult> ReadContent(int id)
{
    int employeeId = 1;

    bool canAccess =
        await _service.CanAccessContentAsync(
            id,
            employeeId);

    if (!canAccess)
    {
        TempData["Error"] =
            "Your request is not approved yet.";

        return RedirectToAction(nameof(Index));
    }

    var content =
        await _service.GetContentByIdAsync(id);

    if (content == null)
        return NotFound();

    return View(content);
}


}