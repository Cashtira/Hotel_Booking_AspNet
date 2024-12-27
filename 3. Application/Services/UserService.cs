namespace _3._Application.Services;

using _2._Domain.Entities;
using _2._Domain.Interfaces.Repositories;
using _3._Application.DTOs;
using _3._Application.Interfaces.Services;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

public class UserService(IUserRepository userRepository, IMapper mapper, UserManager<User> userManager) : IUserService
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IMapper _mapper = mapper;
    private readonly UserManager<User> _userManager = userManager;


    public async Task<List<UserDTO>> GetAllUsersAsync()
    {
        var users = await _userRepository.GetAllUsersAsync().ConfigureAwait(false);
        var userDTOs = new List<UserDTO>();

        foreach (var user in users)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var userDto = _mapper.Map<UserDTO>(user);
            userDto.Roles = roles.ToList(); // Add roles to the user DTO
            userDTOs.Add(userDto);
        }

        return userDTOs;
    }

    // Get user by ID with roles
    public async Task<UserDTO?> GetUserByIdAsync(string userId)
    {
        var user = await _userRepository.GetUserByIdAsync(userId).ConfigureAwait(false);
        if (user == null)
            return null;

        var roles = await _userManager.GetRolesAsync(user);
        var userDto = _mapper.Map<UserDTO>(user);
        userDto.Roles = roles.ToList(); // Add roles to the user DTO

        return userDto;
    }

    // Add a user with roles
    public async Task AddUserAsync(UserDTO userDto)
    {
        var user = _mapper.Map<User>(userDto);
        await _userRepository.AddUserAsync(user);
        // Assign roles to the new user
        foreach (var role in userDto.Roles)
        {
            await _userManager.AddToRoleAsync(user, role);
        }
    }

    // Update a user with roles
    public async Task UpdateUserAsync(UserDTO userDto)
    {
        var user = _mapper.Map<User>(userDto);
        await _userRepository.UpdateUserAsync(user).ConfigureAwait(false);

        // Ensure the roles are updated
        var currentRoles = await _userManager.GetRolesAsync(user);
        var rolesToAdd = userDto.Roles.Except(currentRoles).ToList();
        var rolesToRemove = currentRoles.Except(userDto.Roles).ToList();

        foreach (var role in rolesToAdd)
        {
            await _userManager.AddToRoleAsync(user, role);
        }

        foreach (var role in rolesToRemove)
        {
            await _userManager.RemoveFromRoleAsync(user, role);
        }
    }

    public async Task DeleteUserByIdAsync(string userId)
    {
        await _userRepository.DeleteUserByIdAsync(userId).ConfigureAwait(false);
    }
}
