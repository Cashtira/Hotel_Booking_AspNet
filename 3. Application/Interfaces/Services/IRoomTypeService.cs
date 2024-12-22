namespace _3._Application.Interfaces.Services;

using _3._Application.DTOs;

public interface IRoomTypeService
{
    Task<List<RoomTypeDTO>> GetAllRoomTypesAsync();

    Task<RoomTypeDTO?> GetRoomTypeByIdAsync(int roomTypeId);

    Task AddRoomTypeAsync(RoomTypeDTO roomTypeDto);

    Task UpdateRoomTypeAsync(RoomTypeDTO roomTypeDto);

    Task DeleteRoomTypeAsync(int roomTypeId);
}
