namespace _3._Application.Interfaces.Services;

using _3._Application.DTOs;

public interface IRoleService
{
    Task<List<RoleDTO>> GetAllRolesAsync();

    Task<RoleDTO?> GetRoleByIdAsync(int roleId);

    Task AddRoleAsync(RoleDTO roleDto);

    Task UpdateRoleAsync(RoleDTO roleDto);

    Task DeleteRoleAsync(int roleId);
}
