<<<<<<< HEAD
﻿namespace _1._Infrastructure.Persistence.Repositories
{
    internal interface IBookingRepository
    {
    }
}
=======
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
>>>>>>> 53d271918211a031b688d1dcaa1042bc0d52d567
