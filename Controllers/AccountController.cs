using EasyChatBackend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EasyChatBackend.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AccountController(ILogger<AccountController> logger) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromForm] RegisterRequest request)
    {
        logger.LogInformation("Register called with email: {Email}, nickname: {Nickname}",
            request.Email, request.Nickname);

        return Ok(new { Message = "Registration successful" });
    }

    [HttpPost("getSysSetting")]
    public async Task<IActionResult> GetSysSetting([FromHeader]string token)
    {
        logger.LogInformation("GetSysSetting called with token: {Token}", token);

        // Simulate fetching system settings
        var sysSettings = new
        {
            Version = "1.0.0",
            MaintenanceMode = false,
            MaxUsers = 1000
        };

        return Ok(sysSettings);
    }
}
