namespace _3._Application.Services;

using _2._Domain.Entities;
using _2._Domain.Interfaces.Repositories;
using _3._Application.DTOs;
using _3._Application.Interfaces.Services;
using AutoMapper;

public class BookingService(IBookingRepository bookingRepository, IMapper mapper) : IBookingService
{
    private readonly IBookingRepository bookingRepository = bookingRepository;
    private readonly IMapper mapper = mapper;

    public async Task<List<BookingDTO>> GetAllBookingsAsync()
    {
        var bookings = await this.bookingRepository.GetAllBookingsAsync().ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
        return this.mapper.Map<List<BookingDTO>>(bookings);
    }

    public async Task<BookingDTO?> GetBookingByIdAsync(int bookingId)
    {
        var booking = await this.bookingRepository.GetBookingByIdAsync(bookingId).ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
        return this.mapper.Map<BookingDTO>(booking);
    }

    public async Task AddBookingAsync(BookingDTO bookingDto)
    {
        var booking = this.mapper.Map<Booking>(bookingDto);
        await this.bookingRepository.AddBookingAsync(booking).ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
    }

    public async Task UpdateBookingAsync(BookingDTO bookingDto)
    {
        var booking = this.mapper.Map<Booking>(bookingDto);
        await this.bookingRepository.UpdateBookingAsync(booking).ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
    }

    public async Task DeleteBookingByIdAsync(int bookingId)
    {
        await this.bookingRepository.DeleteBookingByIdAsync(bookingId).ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
    }
}
