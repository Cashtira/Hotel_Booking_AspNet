namespace _3._Application.DTOs;

public sealed class RoleDTO
{
    public int RoleId { get; set; }

    public required IList<UserDTO> UserDTOs { get; set; } = [];
}
