namespace SLMS.Shared.DTOs;

// DTO used for Forgot Password functionality
// Sent from frontend to API when user wants to reset password
public class ForgotPasswordDto
{
    // Employee number used to verify user identity
    public string EmployeeNumber { get; set; } = string.Empty;

    // Username of the account requesting password reset
    public string Username { get; set; } = string.Empty;

    // New password that will replace the old password
    public string NewPassword { get; set; } = string.Empty;
}