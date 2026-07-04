using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SLMS.BLL.Interfaces;
using SLMS.Shared.DTOs.AI;

namespace SLMS.WebAPI.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class AIController : ControllerBase
{
    private readonly IAIService _aiService;

    public AIController(
        IAIService aiService)
    {
        _aiService = aiService;
    }

    [HttpPost("ask")]
    public async Task<IActionResult> Ask(
        [FromBody] AIRequestDto request)
    {
        var response =
            await _aiService.AskAsync(request);

        return Ok(response);
    }
}