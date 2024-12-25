namespace _4._Presentation.Controllers;

using _3._Application.DTOs;
using _3._Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class AuthController(IAuthService authService) : ControllerBase
{
    private readonly IAuthService _authService = authService;

    // POST: api/Auth/register
    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterDTO registerDto)
    {
        if (!ModelState.IsValid || registerDto == null)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var result = await _authService.RegisterAsync(registerDto);
            return Ok(new { Message = result });
        }
        catch (OperationCanceledException ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }

    // POST: api/Auth/login
    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync([FromBody] LoginDTO loginDto)
    {
        if (!ModelState.IsValid || loginDto == null)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var token = await _authService.LoginAsync(loginDto);
            return Ok(new { Token = token });
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { Message = ex.Message });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }
}
