
﻿namespace _2._Domain.Interfaces.Repositories;

using _2._Domain.Entities;

public interface IBookingRepository
{
    Task<IEnumerable<Booking>> GetAllBookingsAsync();

    Task<Booking?> GetBookingByIdAsync(int bookingId);

    Task AddBookingAsync(Booking booking);

    Task UpdateBookingAsync(Booking booking);

    Task DeleteBookingByIdAsync(int bookingId);
}
