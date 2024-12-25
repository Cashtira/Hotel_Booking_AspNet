namespace _2._Domain.Interfaces.Repositories;

using _2._Domain.Entities;

public interface IRoomTypeRepository
{
    Task<IEnumerable<RoomType>> GetAllRoomTypesAsync();

    Task<RoomType?> GetRoomTypeByIdAsync(int roomTypeId);

    Task AddRoomTypeAsync(RoomType roomType);

    Task UpdateRoomTypeAsync(RoomType roomType);

    Task DeleteRoomTypeByIdAsync(int roomTypeId);
}
