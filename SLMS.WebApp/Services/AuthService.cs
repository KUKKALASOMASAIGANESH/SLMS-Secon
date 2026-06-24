using System.Net.Http.Json;
using SLMS.Shared.DTOs;
using SLMS.WebApp.ViewModels;

namespace SLMS.WebApp.Services;

public class AuthService
{
    private readonly HttpClient _httpClient;

    public AuthService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<LoginResponse?> LoginAsync(LoginDto dto)
    {
        var response = await _httpClient.PostAsJsonAsync(
            "api/Auth/login",
            dto);

        return await response.Content
            .ReadFromJsonAsync<LoginResponse>();
    }

    public async Task<ApiResponse> RegisterAsync(RegisterDto dto)
    {
        var response = await _httpClient.PostAsJsonAsync(
            "api/Auth/register",
            dto);

        return await response.Content
            .ReadFromJsonAsync<ApiResponse>();
    }

    public async Task<ApiResponse> ForgotPasswordAsync(
        ForgotPasswordDto dto)
    {
        var response = await _httpClient.PostAsJsonAsync(
            "api/Auth/forgot-password",
            dto);

        return await response.Content
            .ReadFromJsonAsync<ApiResponse>();
    }
}