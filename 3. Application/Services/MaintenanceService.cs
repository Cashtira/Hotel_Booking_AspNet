namespace _3._Application.Services;

using _2._Domain.Entities;
using _2._Domain.Interfaces.Repositories;
using _3._Application.DTOs;
using _3._Application.Interfaces.Services;
using AutoMapper;

public class MaintenanceService(IMaintenanceRepository maintenanceRepository, IMapper mapper) : IMaintenanceService
{
    private readonly IMaintenanceRepository maintenanceRepository = maintenanceRepository;
    private readonly IMapper mapper = mapper;

    public async Task<List<MaintenanceDTO>> GetAllMaintenancesAsync()
    {
        var maintenances = await this.maintenanceRepository.GetAllMaintenancesAsync().ConfigureAwait(true);
        return this.mapper.Map<List<MaintenanceDTO>>(maintenances);
    }

    public async Task<MaintenanceDTO?> GetMaintenanceByIdAsync(int maintenanceId)
    {
        var maintenance = await this.maintenanceRepository.GetMaintenanceByIdAsync(maintenanceId).ConfigureAwait(true);
        return this.mapper.Map<MaintenanceDTO>(maintenance);
    }

    public async Task AddMaintenanceAsync(MaintenanceDTO maintenanceDto)
    {
        var maintenance = this.mapper.Map<Maintenance>(maintenanceDto);
        await this.maintenanceRepository.AddMaintenanceAsync(maintenance).ConfigureAwait(true);
    }

    public async Task UpdateMaintenanceAsync(MaintenanceDTO maintenanceDto)
    {
        var maintenance = this.mapper.Map<Maintenance>(maintenanceDto);
        await this.maintenanceRepository.UpdateMaintenanceAsync(maintenance).ConfigureAwait(true);
    }

    public async Task DeleteMaintenanceByIdAsync(int maintenanceId)
    {
        await this.maintenanceRepository.DeleteMaintenanceByIdAsync(maintenanceId).ConfigureAwait(true);
    }
}
