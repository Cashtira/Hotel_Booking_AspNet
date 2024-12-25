namespace _1._Infrastructure.Persistence.Repositories;

using System.Collections.Generic;
using System.Threading.Tasks;
using _2._Domain.Entities;
using _2._Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

public class RoomRepository(ApplicationDbContext dbContext) : IRoomRepository
{
    private readonly ApplicationDbContext dbContext = dbContext ?? new ApplicationDbContext();

    public async Task<IEnumerable<Room>> GetAllRoomsAsync()
    {
        return await this.dbContext.Rooms.ToListAsync().ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
    }

    public async Task<Room?> GetRoomByIdAsync(int roomId)
    {
        return await this.dbContext.Rooms.FirstOrDefaultAsync(e => e.RoomId == roomId).ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
    }

    public async Task AddRoomAsync(Room room)
    {
        this.dbContext.Add(room);
        await this.dbContext.SaveChangesAsync().ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
    }
    public async Task UpdateRoomAsync(Room room)
    {
        this.dbContext.Update(room);
        await this.dbContext.SaveChangesAsync().ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
    }

    public async Task DeleteRoomByIdAsync(int roomId)
    {
        var room = await this.GetRoomByIdAsync(roomId).ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
        if (room != null)
        {
            this.dbContext.Remove(room);
            await this.dbContext.SaveChangesAsync().ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
        }
    }
}
