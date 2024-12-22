namespace _3._Application.Interfaces.Services;

using _3._Application.DTOs;

public interface IBookingService
{
    Task<List<BookingDTO>> GetAllBookingsAsync();

    Task<BookingDTO?> GetBookingByIdAsync(int bookingId);

    Task AddBookingAsync(BookingDTO bookingDto);

    Task UpdateBookingAsync(BookingDTO bookingDto);

    Task DeleteBookingAsync(int bookingId);
}
