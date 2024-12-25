namespace _1._Infrastructure.Persistence.Repositories;

using System.Collections.Generic;
using System.Threading.Tasks;
using _2._Domain.Entities;
using _2._Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

public class BookingRepository(ApplicationDbContext dbContext) : IBookingRepository
{
    private readonly ApplicationDbContext dbContext = dbContext ?? new ApplicationDbContext();

    public async Task<IEnumerable<Booking>> GetAllBookingsAsync()
    {
        return await this.dbContext.Bookings.ToListAsync().ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
    }

    public async Task<Booking?> GetBookingByIdAsync(int bookingId)
    {
        return await this.dbContext.Bookings.FirstOrDefaultAsync(e => e.BookingId == bookingId).ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
    }

    public async Task AddBookingAsync(Booking booking)
    {
        this.dbContext.Add(booking);
        await this.dbContext.SaveChangesAsync().ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
    }
    public async Task UpdateBookingAsync(Booking booking)
    {
        this.dbContext.Update(booking);
        await this.dbContext.SaveChangesAsync().ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
    }

    public async Task DeleteBookingByIdAsync(int bookingId)
    {
        var booking = await this.GetBookingByIdAsync(bookingId).ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
        if (booking != null)
        {
            this.dbContext.Remove(booking);
            await this.dbContext.SaveChangesAsync().ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
        }
    }
}
