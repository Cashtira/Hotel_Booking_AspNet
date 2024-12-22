namespace _3._Application.Interfaces.Services;

using _3._Application.DTOs;

public interface IRoomService
{
    Task<List<RoomDTO>> GetAllRoomsAsync();

    Task<RoomDTO?> GetRoomByIdAsync(int roomId);

    Task AddRoomAsync(RoomDTO roomDto);

    Task UpdateRoomAsync(RoomDTO roomDto);

    Task DeleteRoomAsync(int roomId);
}
