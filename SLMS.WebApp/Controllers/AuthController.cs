using Microsoft.AspNetCore.Mvc;
using SLMS.Shared.DTOs;
using SLMS.WebApp.Services;
using SLMS.WebApp.ViewModels;

namespace SLMS.WebApp.Controllers;

public class AuthController : Controller
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginDto dto)
    {
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

        HttpContext.Session.SetString(
            "accesstoken",
            result.Token);

        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var dto = new RegisterDto
        {
            EmployeeNumber = model.EmployeeNumber,
            Username = model.Username,
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

    [HttpGet]
    public IActionResult ForgotPassword()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> ForgotPassword(
        ForgotPasswordDto dto)
    {
        var result =
            await _authService.ForgotPasswordAsync(dto);

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

        TempData["Success"] =
            "Password reset successful. Please login.";

        return RedirectToAction("Login");
    }
}