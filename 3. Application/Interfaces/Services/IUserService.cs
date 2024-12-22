namespace _3._Application.Interfaces.Services;

using _3._Application.DTOs;

public interface IUserService
{
    Task<List<UserDTO>> GetAllUsersAsync();

    Task<UserDTO?> GetUserByIdAsync(int userId);

    Task AddUserAsync(UserDTO userDto);

    Task UpdateUserAsync(UserDTO userDto);

    Task DeleteUserAsync(int userId);
}
