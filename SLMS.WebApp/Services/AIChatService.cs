using System.Net.Http.Json;
using SLMS.Shared.DTOs.AI;

namespace SLMS.WebApp.Services;

public class AIChatService
{
    private readonly HttpClient _httpClient;

    public AIChatService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> AskAsync(string question)
    {
        var request = new AIRequestDto
        {
            Question = question
        };

        var response =
            await _httpClient.PostAsJsonAsync(
                "api/AI/ask",
                request);

        response.EnsureSuccessStatusCode();

        var result =
            await response.Content.ReadFromJsonAsync<AIResponseDto>();

        return result?.Answer ?? "No response.";
    }
}