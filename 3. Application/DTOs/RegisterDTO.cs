namespace _3._Application.DTOs;

public sealed class RegisterDTO
{
    public required string Username { get; set; }

    public required string Email { get; set; }

    public required string FullName { get; set; }

    public required string Password { get; set; }

    public required string PhoneNumber { get; set; }
}
