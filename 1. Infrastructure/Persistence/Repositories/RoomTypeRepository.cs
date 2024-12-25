namespace _1._Infrastructure.Persistence.Repositories;

using System.Collections.Generic;
using System.Threading.Tasks;
using _2._Domain.Entities;
using _2._Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

public class RoomTypeRepository(ApplicationDbContext dbContext) : IRoomTypeRepository
{
    private readonly ApplicationDbContext dbContext = dbContext ?? new ApplicationDbContext();

    public async Task<IEnumerable<RoomType>> GetAllRoomTypesAsync()
    {
        return await this.dbContext.RoomTypes.ToListAsync().ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
    }

    public async Task<RoomType?> GetRoomTypeByIdAsync(int roomTypeId)
    {
        return await this.dbContext.RoomTypes.FirstOrDefaultAsync(e => e.RoomTypeId == roomTypeId).ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
    }

    public async Task AddRoomTypeAsync(RoomType roomType)
    {
        this.dbContext.Add(roomType);
        await this.dbContext.SaveChangesAsync().ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
    }
    public async Task UpdateRoomTypeAsync(RoomType roomType)
    {
        this.dbContext.Update(roomType);
        await this.dbContext.SaveChangesAsync().ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
    }

    public async Task DeleteRoomTypeByIdAsync(int roomTypeId)
    {
        var roomType = await this.GetRoomTypeByIdAsync(roomTypeId).ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
        if (roomType != null)
        {
            this.dbContext.Remove(roomType);
            await this.dbContext.SaveChangesAsync().ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
        }
    }
}
