namespace _1._Infrastructure.Persistence.Repositories;

using System.Collections.Generic;
using System.Threading.Tasks;
using _2._Domain.Entities;
using _2._Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

public class UserRepository(ApplicationDbContext dbContext) : IUserRepository
{
    private readonly ApplicationDbContext dbContext = dbContext ?? new ApplicationDbContext();

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        return await this.dbContext.Users.ToListAsync().ConfigureAwait(true);
    }

    public async Task<User?> GetUserByIdAsync(int userId)
    {
        return await this.dbContext.Users.FirstOrDefaultAsync(e => e.UserId == userId).ConfigureAwait(true);
    }

    public async Task AddUserAsync(User user)
    {
        this.dbContext.Add(user);
        await this.dbContext.SaveChangesAsync().ConfigureAwait(true);
    }
    public async Task UpdateUserAsync(User user)
    {
        this.dbContext.Update(user);
        await this.dbContext.SaveChangesAsync().ConfigureAwait(true);
    }

    public async Task DeleteUserByIdAsync(int userId)
    {
        var user = await this.GetUserByIdAsync(userId).ConfigureAwait(true);
        if (user != null)
        {
            this.dbContext.Remove(user);
            await this.dbContext.SaveChangesAsync().ConfigureAwait(true);
        }
    }
}
