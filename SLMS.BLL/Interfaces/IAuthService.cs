using SLMS.Shared.DTOs;
using SLMS.Shared.Responses;

namespace SLMS.BLL.Interfaces;

public interface IAuthService
{
	Task<AuthResponse> LoginAsync(LoginDto dto);

	Task<AuthResponse> RegisterAsync(RegisterDto dto);

    Task<AuthResponse> ForgotPasswordAsync(ForgotPasswordDto dto);
}