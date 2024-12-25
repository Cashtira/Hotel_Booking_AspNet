namespace _1._Infrastructure.Persistence.Repositories;

using System.Collections.Generic;
using System.Threading.Tasks;
using _2._Domain.Entities;
using _2._Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

public class LoyaltyProgramRepository(ApplicationDbContext dbContext) : ILoyaltyProgramRepository
{
    private readonly ApplicationDbContext dbContext = dbContext ?? new ApplicationDbContext();

    public async Task<IEnumerable<LoyaltyProgram>> GetAllLoyaltyProgramsAsync()
    {
        return await this.dbContext.LoyaltyPrograms.ToListAsync().ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
    }

    public async Task<LoyaltyProgram?> GetLoyaltyProgramByIdAsync(int loyaltyProgramId)
    {
        return await this.dbContext.LoyaltyPrograms.FirstOrDefaultAsync(e => e.LoyaltyProgramId == loyaltyProgramId).ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
    }

    public async Task AddLoyaltyProgramAsync(LoyaltyProgram loyaltyProgram)
    {
        this.dbContext.Add(loyaltyProgram);
        await this.dbContext.SaveChangesAsync().ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
    }
    public async Task UpdateLoyaltyProgramAsync(LoyaltyProgram loyaltyProgram)
    {
        this.dbContext.Update(loyaltyProgram);
        await this.dbContext.SaveChangesAsync().ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
    }

    public async Task DeleteLoyaltyProgramByIdAsync(int loyaltyProgramId)
    {
        var loyaltyProgram = await this.GetLoyaltyProgramByIdAsync(loyaltyProgramId).ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
        if (loyaltyProgram != null)
        {
            this.dbContext.Remove(loyaltyProgram);
            await this.dbContext.SaveChangesAsync().ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
        }
    }
}
