namespace _1._Infrastructure.Persistence.Repositories;

using System.Collections.Generic;
using System.Threading.Tasks;
using _2._Domain.Entities;
using _2._Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

public class HousekeepingRepository(ApplicationDbContext dbContext) : IHousekeepingRepository
{
    private readonly ApplicationDbContext dbContext = dbContext ?? new ApplicationDbContext();

    public async Task<IEnumerable<Housekeeping>> GetAllHousekeepingsAsync()
    {
        return await this.dbContext.Housekeepings.ToListAsync().ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
    }

    public async Task<Housekeeping?> GetHousekeepingByIdAsync(int housekeepingId)
    {
        return await this.dbContext.Housekeepings.FirstOrDefaultAsync(e => e.HousekeepingId == housekeepingId).ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
    }

    public async Task AddHousekeepingAsync(Housekeeping housekeeping)
    {
        this.dbContext.Add(housekeeping);
        await this.dbContext.SaveChangesAsync().ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
    }
    public async Task UpdateHousekeepingAsync(Housekeeping housekeeping)
    {
        this.dbContext.Update(housekeeping);
        await this.dbContext.SaveChangesAsync().ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
    }

    public async Task DeleteHousekeepingByIdAsync(int housekeepingId)
    {
        var housekeeping = await this.GetHousekeepingByIdAsync(housekeepingId).ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
        if (housekeeping != null)
        {
            this.dbContext.Remove(housekeeping);
            await this.dbContext.SaveChangesAsync().ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
        }
    }
}
