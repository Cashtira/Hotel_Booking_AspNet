namespace _2._Domain.Interfaces.Repositories;

using _2._Domain.Entities;

public interface IMaintenanceRepository
{
    Task<IEnumerable<Maintenance>> GetAllMaintenancesAsync();

    Task<Maintenance?> GetMaintenanceByIdAsync(int maintenanceId);

    Task AddMaintenanceAsync(Maintenance maintenance);

    Task UpdateMaintenanceAsync(Maintenance maintenance);

    Task DeleteMaintenanceByIdAsync(int maintenanceId);
}
