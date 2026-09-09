using SLMS.Shared.DTOs.AI;

namespace SLMS.BLL.Interfaces;

public interface IAIService
{
    Task<AIResponseDto> AskAsync(AIRequestDto request);
}