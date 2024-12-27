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

public sealed class AuthService : IAuthService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IConfiguration _configuration;

    // Constructor injection
    public AuthService(UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;
    }

    // Register user and assign role
    public async Task<string> RegisterAsync(RegisterDTO registerDto)
    {
        var user = new User
        {
            UserName = registerDto.Username,
            Email = registerDto.Email,
            FullName = registerDto.FullName,
        };

        var result = await _userManager.CreateAsync(user, registerDto.Password);
        if (result.Succeeded)
        {
            // Assign default role (e.g., "Customer")
            await _userManager.AddToRoleAsync(user, "Customer");

            return "User registered successfully!";
        }

        // Return detailed error message if registration fails
        throw new OperationCanceledException($"Registration failed: {string.Join(", ", result.Errors.Select(e => e.Description))}");
    }

    // Login user and generate JWT token
    public async Task<string> LoginAsync(LoginDTO loginDto)
    {
        var user = await _userManager.FindByNameAsync(loginDto.Username);
        if (user == null)
        {
            throw new KeyNotFoundException("User not found");
        }

        var result = await _signInManager.PasswordSignInAsync(user, loginDto.Password, false, false);
        if (result.Succeeded)
        {
            // Generate JWT token if login is successful
            var token = await GenerateJwtTokenAsync(user.UserName);
            return token;
        }

        // Invalid login attempt
        throw new UnauthorizedAccessException("Invalid login attempt");
    }

    // Generate JWT token with user claims
    public async Task<string> GenerateJwtTokenAsync(string userName)
    {
        var user = await _userManager.FindByNameAsync(userName);
        if (user == null)
        {
            throw new KeyNotFoundException("User not found");
        }

        var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, user.UserName),
        new Claim(ClaimTypes.Email, user.Email),
        new Claim(ClaimTypes.NameIdentifier, user.Id)
    };

        // Get user's roles
        var roles = await _userManager.GetRolesAsync(user);
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            _configuration["Jwt:Issuer"],
            _configuration["Jwt:Audience"],
            claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
