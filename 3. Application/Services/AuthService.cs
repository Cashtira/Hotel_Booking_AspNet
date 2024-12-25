namespace _3._Application.Services;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using _2._Domain.Entities;
using _3._Application.DTOs;
using _3._Application.Interfaces.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

public sealed class AuthService(UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration configuration) : IAuthService
{
    private readonly UserManager<User> userManager = userManager;
    private readonly SignInManager<User> signInManager = signInManager;
    private readonly IConfiguration configuration = configuration;

    public async Task<string> RegisterAsync(RegisterDTO registerDto)
    {
        var user = new User
        {
            UserName = registerDto.Username,
            Email = registerDto.Email,
            FullName = registerDto.FullName,
        };

        var result = await this.userManager.CreateAsync(user, registerDto.Password).ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
        if (result.Succeeded)
        {
            return "User registered successfully!";
        }

        throw new OperationCanceledException("Registration failed");
    }

    public async Task<string> LoginAsync(LoginDTO loginDto)
    {
        var user = await this.userManager.FindByNameAsync(loginDto.Username).ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
        if (user == null)
        {
            throw new Exception("User not found");
        }

        var result = await this.signInManager.PasswordSignInAsync(user, loginDto.Password, false, false).ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
        if (result.Succeeded)
        {
            var token = await GenerateJwtTokenAsync(user.UserName).ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
            return token;
        }

        throw new Exception("Invalid login attempt");
    }

    public async Task<string> GenerateJwtTokenAsync(string userName)
    {
        var user = await this.userManager.FindByNameAsync(userName).ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName),
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.configuration["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            this.configuration["Jwt:Issuer"],
            this.configuration["Jwt:Audience"],
            claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
