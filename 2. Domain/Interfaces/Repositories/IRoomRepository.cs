namespace _2._Domain.Interfaces.Repositories;

using _2._Domain.Entities;

public interface IRoomRepository
{
    Task<IEnumerable<Room>> GetAllRoomsAsync();

    Task<Room?> GetRoomByIdAsync(int roomId);

    Task AddRoomAsync(Room room);

    Task UpdateRoomAsync(Room room);

    Task DeleteRoomByIdAsync(int roomId);
}
