using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SLMS.WebApp.Services;

namespace SLMS.WebApp.Controllers;

[Authorize]
public class AIController : Controller
{
    private readonly AIChatService _aiService;

    public AIController(AIChatService aiService)
    {
        _aiService = aiService;
    }

    [HttpPost]
    public async Task<IActionResult> Ask(string question)
    {
        var answer =
            await _aiService.AskAsync(question);

        return Json(new
        {
            answer
        });
    }
}