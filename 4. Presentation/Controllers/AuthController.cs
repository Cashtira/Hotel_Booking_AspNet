using _3._Application.DTOs;
using _3._Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace _4._Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(IAuthService authService) : ControllerBase
{
    private readonly IAuthService authService = authService;

    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterDTO registerDto)
    {
        var result = await this.authService.RegisterAsync(registerDto).ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync([FromBody] LoginDTO loginDto)
    {
        var token = await this.authService.LoginAsync(loginDto).ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
        return Ok(new { Token = token });
    }
}
