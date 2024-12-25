namespace _3._Application.Interfaces.Services;

using _3._Application.DTOs;

public interface IMaintenanceService
{
    Task<List<MaintenanceDTO>> GetAllMaintenancesAsync();

    Task<MaintenanceDTO?> GetMaintenanceByIdAsync(int loyaltyProgramId);

    Task AddMaintenanceAsync(MaintenanceDTO loyaltyProgramDto);

    Task UpdateMaintenanceAsync(MaintenanceDTO loyaltyProgramDto);

    Task DeleteMaintenanceByIdAsync(int loyaltyProgramId);
}
