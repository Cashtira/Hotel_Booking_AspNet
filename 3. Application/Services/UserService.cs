namespace _3._Application.Services;

using _2._Domain.Entities;
using _2._Domain.Interfaces.Repositories;
using _3._Application.DTOs;
using _3._Application.Interfaces.Services;
using AutoMapper;

public class UserService(IUserRepository userRepository, IMapper mapper) : IUserService
{
    private readonly IUserRepository userRepository = userRepository;
    private readonly IMapper mapper = mapper;

    public async Task<List<UserDTO>> GetAllUsersAsync()
    {
        var users = await this.userRepository.GetAllUsersAsync().ConfigureAwait(true);
        return this.mapper.Map<List<UserDTO>>(users);
    }

    public async Task<UserDTO?> GetUserByIdAsync(int userId)
    {
        var user = await this.userRepository.GetUserByIdAsync(userId).ConfigureAwait(true);
        return this.mapper.Map<UserDTO>(user);
    }

    public async Task AddUserAsync(UserDTO userDto)
    {
        var user = this.mapper.Map<User>(userDto);
        await this.userRepository.AddUserAsync(user).ConfigureAwait(true);
    }

    public async Task UpdateUserAsync(UserDTO userDto)
    {
        var user = this.mapper.Map<User>(userDto);
        await this.userRepository.UpdateUserAsync(user).ConfigureAwait(true);
    }

    public async Task DeleteUserByIdAsync(int userId)
    {
        await this.userRepository.DeleteUserByIdAsync(userId).ConfigureAwait(true);
    }
}
