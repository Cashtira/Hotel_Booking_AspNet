namespace _2._Domain.Interfaces.Repositories;

using _2._Domain.Entities;

public interface IHousekeepingRepository
{
    Task<IEnumerable<Housekeeping>> GetAllHousekeepingsAsync();

    Task<Housekeeping?> GetHousekeepingByIdAsync(int housekeepingId);

    Task AddHousekeepingAsync(Housekeeping housekeeping);

    Task UpdateHousekeepingAsync(Housekeeping housekeeping);

    Task DeleteHousekeepingByIdAsync(int housekeepingId);
}
