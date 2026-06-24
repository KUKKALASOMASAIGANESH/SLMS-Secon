using Microsoft.AspNetCore.Mvc;
using SLMS.Shared.DTOs;
using SLMS.WebApp.Services;
using SLMS.WebApp.ViewModels;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

namespace SLMS.WebApp.Controllers;

// Authentication controller handles:
// Login, Register, Forgot Password, Logout operations
// Uses AuthService to communicate with backend API
[AllowAnonymous]
public class AuthController : Controller
{
    private readonly AuthService _authService;

    // Inject authentication service
    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    // Loads Login page
    [HttpGet]
    public IActionResult Login()
    {
        // If user is already authenticated, redirect to Home
        if (User.Identity?.IsAuthenticated == true)
            return RedirectToAction("Index", "Home");

       //return Content(
        //$"Authenticated = {User.Identity?.IsAuthenticated}");
        return View();
    }

    // Handles Login form submission
    [HttpPost]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        // Call API to validate user credentials
        var result = await _authService.LoginAsync(dto);

        if (result == null)
        {
            ViewBag.Error = "Unable to connect to API";
            return View(dto);
        }

        if (!result.Success)
        {
            ViewBag.Error = result.Message;
            return View(dto);
        }

        // Store JWT token in session for API calls
        HttpContext.Session.SetString("accesstoken", result.Token);

        Console.WriteLine($"Role from API = {result.Role}");

        var claims = new List<Claim>
{
    new Claim(ClaimTypes.Name, dto.Username),
    new Claim(ClaimTypes.Role, result.Role)
};

        var identity = new ClaimsIdentity(
            claims,
            CookieAuthenticationDefaults.AuthenticationScheme);

        var principal = new ClaimsPrincipal(identity);

        // Sign in user using cookie authentication
        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            principal);

        // Redirect to Home page after successful login
        return RedirectToAction("Index", "Home");
    }

    // Loads Register page
    [HttpGet]
    public IActionResult Register()
    {
        // Prevent access if already logged in
        if (User.Identity?.IsAuthenticated == true)
            return RedirectToAction("Index", "Home");

        return View();
    }

    // Handles Register form submission
    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var dto = new RegisterDto
        {
           /* EmployeeNumber = model.EmployeeNumber,
            Username = model.Username,
            Password = model.Password*/
           

        EmployeeNumber = model.EmployeeNumber.Trim(),
        EmployeeName = model.EmployeeName.Trim(),
        Username = model.Username.Trim(),
        Password = model.Password
        };

        var result = await _authService.RegisterAsync(dto);

        if (result == null)
        {
            ViewBag.Error = "Unable to connect to API";
            return View(model);
        }

        if (!result.Success)
        {
            ViewBag.Error = result.Message;
            return View(model);
        }

        TempData["Success"] = "Registration successful. Please login.";

        return RedirectToAction("Login");
    }

    // Loads Forgot Password page
    [HttpGet]
    public IActionResult ForgotPassword()
    {
        return View();
    }

    // Handles Forgot Password submission
    [HttpPost]
    public async Task<IActionResult> ForgotPassword(ForgotPasswordDto dto)
    {
        var result = await _authService.ForgotPasswordAsync(dto);

        if (result == null)
        {
            ViewBag.Error = "Unable to connect to API";
            return View(dto);
        }

        if (!result.Success)
        {
            ViewBag.Error = result.Message;
            return View(dto);
        }

        TempData["Success"] = "Password reset successful. Please login.";

        return RedirectToAction("Login");
    }

    // Logs out user and clears authentication session
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(
            CookieAuthenticationDefaults.AuthenticationScheme);

        HttpContext.Session.Clear();

        return RedirectToAction("Login");
    }


    [HttpGet]
    public IActionResult AccessDenied()
    {
        return Content("Access Denied: You do not have permission to access this page.");
    }

}