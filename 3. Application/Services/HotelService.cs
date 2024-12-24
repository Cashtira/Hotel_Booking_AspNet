namespace _3._Application.Services;

using _2._Domain.Entities;
using _2._Domain.Interfaces.Repositories;
using _3._Application.DTOs;
using _3._Application.Interfaces.Services;
using AutoMapper;

public class HotelService(IHotelRepository hotelRepository, IMapper mapper) : IHotelService
{
    private readonly IHotelRepository hotelRepository = hotelRepository;
    private readonly IMapper mapper = mapper;

    public async Task<List<HotelDTO>> GetAllHotelsAsync()
    {
        var hotels = await this.hotelRepository.GetAllHotelsAsync().ConfigureAwait(true);
        return this.mapper.Map<List<HotelDTO>>(hotels);
    }

    public async Task<HotelDTO?> GetHotelByIdAsync(int hotelId)
    {
        var hotel = await this.hotelRepository.GetHotelByIdAsync(hotelId).ConfigureAwait(true);
        return this.mapper.Map<HotelDTO>(hotel);
    }

    public async Task AddHotelAsync(HotelDTO hotelDto)
    {
        var hotel = this.mapper.Map<Hotel>(hotelDto);
        await this.hotelRepository.AddHotelAsync(hotel).ConfigureAwait(true);
    }

    public async Task UpdateHotelAsync(HotelDTO hotelDto)
    {
        var hotel = this.mapper.Map<Hotel>(hotelDto);
        await this.hotelRepository.UpdateHotelAsync(hotel).ConfigureAwait(true);
    }

    public async Task DeleteHotelByIdAsync(int hotelId)
    {
        await this.hotelRepository.DeleteHotelByIdAsync(hotelId).ConfigureAwait(true);
    }
}
