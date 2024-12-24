namespace _3._Application.Services;

using _2._Domain.Entities;
using _2._Domain.Interfaces.Repositories;
using _3._Application.DTOs;
using _3._Application.Interfaces.Services;
using AutoMapper;

public class RoomService(IRoomRepository roomRepository, IMapper mapper) : IRoomService
{
    private readonly IRoomRepository roomRepository = roomRepository;
    private readonly IMapper mapper = mapper;

    public async Task<List<RoomDTO>> GetAllRoomsAsync()
    {
        var rooms = await this.roomRepository.GetAllRoomsAsync().ConfigureAwait(true);
        return this.mapper.Map<List<RoomDTO>>(rooms);
    }

    public async Task<RoomDTO?> GetRoomByIdAsync(int roomId)
    {
        var room = await this.roomRepository.GetRoomByIdAsync(roomId).ConfigureAwait(true);
        return this.mapper.Map<RoomDTO>(room);
    }

    public async Task AddRoomAsync(RoomDTO roomDto)
    {
        var room = this.mapper.Map<Room>(roomDto);
        await this.roomRepository.AddRoomAsync(room).ConfigureAwait(true);
    }

    public async Task UpdateRoomAsync(RoomDTO roomDto)
    {
        var room = this.mapper.Map<Room>(roomDto);
        await this.roomRepository.UpdateRoomAsync(room).ConfigureAwait(true);
    }

    public async Task DeleteRoomByIdAsync(int roomId)
    {
        await this.roomRepository.DeleteRoomByIdAsync(roomId).ConfigureAwait(true);
    }
}
