/*
 * ApiResponse<T>
 *
 * Purpose:
 * Provides a standardized structure for all API responses.
 *
 * Responsibilities:
 * - Indicates request success or failure
 * - Returns response messages
 * - Carries response data payload
 * - Returns validation or business errors
 *
 * Response Structure:
 * {
 *     success,
 *     message,
 *     data,
 *     errors
 * }
 *
 * This wrapper ensures consistent response formatting
 * across all API endpoints and simplifies client-side handling.
 */

namespace SLMS.Shared.Responses;

public class ApiResponse<T>
{
    public bool Success { get; set; }

    public string Message { get; set; } = string.Empty;

    public T? Data { get; set; }

    public List<string>? Errors { get; set; }
}