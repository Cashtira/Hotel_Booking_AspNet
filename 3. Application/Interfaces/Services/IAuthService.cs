using _3._Application.DTOs;

namespace _3._Application.Interfaces.Services;

public interface IAuthService
{
    Task<string> RegisterAsync(RegisterDTO registerDto);

    Task<string> LoginAsync(LoginDTO loginDto);

    Task<string> GenerateJwtTokenAsync(string userName);
}
