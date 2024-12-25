namespace _1._Infrastructure.Persistence.Repositories;

using System.Collections.Generic;
using System.Threading.Tasks;
using _2._Domain.Entities;
using _2._Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

public class HotelRepository(ApplicationDbContext dbContext) : IHotelRepository
{
    private readonly ApplicationDbContext dbContext = dbContext ?? new ApplicationDbContext();

    public async Task<IEnumerable<Hotel>> GetAllHotelsAsync()
    {
        return await this.dbContext.Hotels.ToListAsync().ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
    }

    public async Task<Hotel?> GetHotelByIdAsync(int hotelId)
    {
        return await this.dbContext.Hotels.FirstOrDefaultAsync(e => e.HotelId == hotelId).ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
    }

    public async Task AddHotelAsync(Hotel hotel)
    {
        this.dbContext.Add(hotel);
        await this.dbContext.SaveChangesAsync().ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
    }
    public async Task UpdateHotelAsync(Hotel hotel)
    {
        this.dbContext.Update(hotel);
        await this.dbContext.SaveChangesAsync().ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
    }

    public async Task DeleteHotelByIdAsync(int hotelId)
    {
        var hotel = await this.GetHotelByIdAsync(hotelId).ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
        if (hotel != null)
        {
            this.dbContext.Remove(hotel);
            await this.dbContext.SaveChangesAsync().ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
        }
    }
}
