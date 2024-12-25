namespace _2._Domain.Interfaces.Repositories;

using _2._Domain.Entities;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllUsersAsync();

    Task<User?> GetUserByIdAsync(string userId);

    Task AddUserAsync(User user);

    Task UpdateUserAsync(User user);

    Task DeleteUserByIdAsync(string userId);
}
