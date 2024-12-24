namespace _2._Domain.Interfaces.Repositories;

using _2._Domain.Entities;

public interface IHotelRepository
{
    Task<IEnumerable<Hotel>> GetAllHotelsAsync();

    Task<Hotel?> GetHotelByIdAsync(int hotelId);

    Task AddHotelAsync(Hotel hotel);

    Task UpdateHotelAsync(Hotel hotel);

    Task DeleteHotelByIdAsync(int hotelId);
}
