namespace _4._Presentation.Controllers;

using _3._Application.DTOs;
using _3._Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    // Constructor injection for IAuthService
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    /// <summary>
    /// Register a new user with provided credentials.
    /// </summary>
    /// <param name="registerDto">User registration data including username, email, and password</param>
    /// <returns>A message indicating the success or failure of the registration process</returns>
    /// <response code="200">Registration successful</response>
    /// <response code="400">Invalid data or user already exists</response>
    [HttpPost("register")]
    [Produces("application/json")]
    [Consumes("application/json")]
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

    /// <summary>
    /// Authenticate a user and return a JWT token.
    /// </summary>
    /// <param name="loginDto">User login data including username and password</param>
    /// <returns>A JWT token if login is successful</returns>
    /// <response code="200">Login successful and token returned</response>
    /// <response code="400">Invalid login data</response>
    /// <response code="401">Unauthorized access, invalid credentials</response>
    [HttpPost("login")]
    [Produces("application/json")]
    [Consumes("application/json")]
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
