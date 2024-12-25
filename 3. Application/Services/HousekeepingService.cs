namespace _3._Application.Services;

using _2._Domain.Entities;
using _2._Domain.Interfaces.Repositories;
using _3._Application.DTOs;
using _3._Application.Interfaces.Services;
using AutoMapper;

public class HousekeepingService(IHousekeepingRepository housekeepingRepository, IMapper mapper) : IHousekeepingService
{
    private readonly IHousekeepingRepository housekeepingRepository = housekeepingRepository;
    private readonly IMapper mapper = mapper;

    public async Task<List<HousekeepingDTO>> GetAllHousekeepingsAsync()
    {
        var housekeepings = await this.housekeepingRepository.GetAllHousekeepingsAsync().ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
        return this.mapper.Map<List<HousekeepingDTO>>(housekeepings);
    }

    public async Task<HousekeepingDTO?> GetHousekeepingByIdAsync(int housekeepingId)
    {
        var housekeeping = await this.housekeepingRepository.GetHousekeepingByIdAsync(housekeepingId).ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
        return this.mapper.Map<HousekeepingDTO>(housekeeping);
    }

    public async Task AddHousekeepingAsync(HousekeepingDTO housekeepingDto)
    {
        var housekeeping = this.mapper.Map<Housekeeping>(housekeepingDto);
        await this.housekeepingRepository.AddHousekeepingAsync(housekeeping).ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
    }

    public async Task UpdateHousekeepingAsync(HousekeepingDTO housekeepingDto)
    {
        var housekeeping = this.mapper.Map<Housekeeping>(housekeepingDto);
        await this.housekeepingRepository.UpdateHousekeepingAsync(housekeeping).ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
    }

    public async Task DeleteHousekeepingByIdAsync(int housekeepingId)
    {
        await this.housekeepingRepository.DeleteHousekeepingByIdAsync(housekeepingId).ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
    }
}
