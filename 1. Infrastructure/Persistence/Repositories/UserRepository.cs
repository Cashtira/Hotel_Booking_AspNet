namespace _1._Infrastructure.Persistence.Repositories;

using System.Collections.Generic;
using System.Threading.Tasks;
using _2._Domain.Entities;
using _2._Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

public class UserRepository(ApplicationDbContext dbContext) : IUserRepository
{
    private readonly ApplicationDbContext dbContext = dbContext;

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        return await this.dbContext.Users.ToListAsync().ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
    }

    public async Task<User?> GetUserByIdAsync(string userId)
    {
        return await this.dbContext.Users.FirstOrDefaultAsync(e => e.Id == userId).ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
    }

    public async Task AddUserAsync(User user)
    {
        this.dbContext.Add(user);
        await this.dbContext.SaveChangesAsync().ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
    }
    public async Task UpdateUserAsync(User user)
    {
        this.dbContext.Update(user);
        await this.dbContext.SaveChangesAsync().ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
    }

    public async Task DeleteUserByIdAsync(string userId)
    {
        var user = await this.GetUserByIdAsync(userId).ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
        if (user != null)
        {
            this.dbContext.Remove(user);
            await this.dbContext.SaveChangesAsync().ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
        }
    }
}
