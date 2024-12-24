namespace _3._Application.Interfaces.Services;

using _3._Application.DTOs;

public interface IHotelService
{
    Task<List<HotelDTO>> GetAllHotelsAsync();

    Task<HotelDTO?> GetHotelByIdAsync(int hotelId);

    Task AddHotelAsync(HotelDTO hotelDto);

    Task UpdateHotelAsync(HotelDTO hotelDto);

    Task DeleteHotelByIdAsync(int hotelId);
}
