namespace _3._Application.Interfaces.Services;

using _3._Application.DTOs;

public interface IHousekeepingService
{
    Task<List<HousekeepingDTO>> GetAllHousekeepingsAsync();

    Task<HousekeepingDTO?> GetHousekeepingByIdAsync(int housekeepingId);

    Task AddHousekeepingAsync(HousekeepingDTO housekeepingDto);

    Task UpdateHousekeepingAsync(HousekeepingDTO housekeepingDto);

    Task DeleteHousekeepingAsync(int housekeepingId);
}
