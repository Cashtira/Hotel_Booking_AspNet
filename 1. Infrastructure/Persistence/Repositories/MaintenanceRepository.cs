namespace _1._Infrastructure.Persistence.Repositories;

using System.Collections.Generic;
using System.Threading.Tasks;
using _2._Domain.Entities;
using _2._Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

public class MaintenanceRepository(ApplicationDbContext dbContext) : IMaintenanceRepository
{
    private readonly ApplicationDbContext dbContext = dbContext ?? new ApplicationDbContext();

    public async Task<IEnumerable<Maintenance>> GetAllMaintenancesAsync()
    {
        return await this.dbContext.Maintenances.ToListAsync().ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
    }

    public async Task<Maintenance?> GetMaintenanceByIdAsync(int maintenanceId)
    {
        return await this.dbContext.Maintenances.FirstOrDefaultAsync(e => e.MaintenanceId == maintenanceId).ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
    }

    public async Task AddMaintenanceAsync(Maintenance maintenance)
    {
        this.dbContext.Add(maintenance);
        await this.dbContext.SaveChangesAsync().ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
    }
    public async Task UpdateMaintenanceAsync(Maintenance maintenance)
    {
        this.dbContext.Update(maintenance);
        await this.dbContext.SaveChangesAsync().ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
    }

    public async Task DeleteMaintenanceByIdAsync(int maintenanceId)
    {
        var maintenance = await this.GetMaintenanceByIdAsync(maintenanceId).ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
        if (maintenance != null)
        {
            this.dbContext.Remove(maintenance);
            await this.dbContext.SaveChangesAsync().ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
        }
    }
}
