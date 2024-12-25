namespace _3._Application.Services;

using _2._Domain.Entities;
using _2._Domain.Interfaces.Repositories;
using _3._Application.DTOs;
using _3._Application.Interfaces.Services;
using AutoMapper;

public class RoomTypeService(IRoomTypeRepository roomTypeRepository, IMapper mapper) : IRoomTypeService
{
    private readonly IRoomTypeRepository roomTypeRepository = roomTypeRepository;
    private readonly IMapper mapper = mapper;

    public async Task<List<RoomTypeDTO>> GetAllRoomTypesAsync()
    {
        var roomTypes = await this.roomTypeRepository.GetAllRoomTypesAsync().ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
        return this.mapper.Map<List<RoomTypeDTO>>(roomTypes);
    }

    public async Task<RoomTypeDTO?> GetRoomTypeByIdAsync(int roomTypeId)
    {
        var roomType = await this.roomTypeRepository.GetRoomTypeByIdAsync(roomTypeId).ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
        return this.mapper.Map<RoomTypeDTO>(roomType);
    }

    public async Task AddRoomTypeAsync(RoomTypeDTO roomTypeDto)
    {
        var roomType = this.mapper.Map<RoomType>(roomTypeDto);
        await this.roomTypeRepository.AddRoomTypeAsync(roomType).ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
    }

    public async Task UpdateRoomTypeAsync(RoomTypeDTO roomTypeDto)
    {
        var roomType = this.mapper.Map<RoomType>(roomTypeDto);
        await this.roomTypeRepository.UpdateRoomTypeAsync(roomType).ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
    }

    public async Task DeleteRoomTypeByIdAsync(int roomTypeId)
    {
        await this.roomTypeRepository.DeleteRoomTypeByIdAsync(roomTypeId).ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
    }
}
