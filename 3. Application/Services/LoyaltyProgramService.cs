namespace _3._Application.Services;

using _2._Domain.Entities;
using _2._Domain.Interfaces.Repositories;
using _3._Application.DTOs;
using _3._Application.Interfaces.Services;
using AutoMapper;

public class LoyaltyProgramService(ILoyaltyProgramRepository loyaltyProgramRepository, IMapper mapper) : ILoyaltyProgramService
{
    private readonly ILoyaltyProgramRepository loyaltyProgramRepository = loyaltyProgramRepository;
    private readonly IMapper mapper = mapper;

    public async Task<List<LoyaltyProgramDTO>> GetAllLoyaltyProgramsAsync()
    {
        var loyaltyPrograms = await this.loyaltyProgramRepository.GetAllLoyaltyProgramsAsync().ConfigureAwait(true);
        return this.mapper.Map<List<LoyaltyProgramDTO>>(loyaltyPrograms);
    }

    public async Task<LoyaltyProgramDTO?> GetLoyaltyProgramByIdAsync(int loyaltyProgramId)
    {
        var loyaltyProgram = await this.loyaltyProgramRepository.GetLoyaltyProgramByIdAsync(loyaltyProgramId).ConfigureAwait(true);
        return this.mapper.Map<LoyaltyProgramDTO>(loyaltyProgram);
    }

    public async Task AddLoyaltyProgramAsync(LoyaltyProgramDTO loyaltyProgramDto)
    {
        var loyaltyProgram = this.mapper.Map<LoyaltyProgram>(loyaltyProgramDto);
        await this.loyaltyProgramRepository.AddLoyaltyProgramAsync(loyaltyProgram).ConfigureAwait(true);
    }

    public async Task UpdateLoyaltyProgramAsync(LoyaltyProgramDTO loyaltyProgramDto)
    {
        var loyaltyProgram = this.mapper.Map<LoyaltyProgram>(loyaltyProgramDto);
        await this.loyaltyProgramRepository.UpdateLoyaltyProgramAsync(loyaltyProgram).ConfigureAwait(true);
    }

    public async Task DeleteLoyaltyProgramByIdAsync(int loyaltyProgramId)
    {
        await this.loyaltyProgramRepository.DeleteLoyaltyProgramByIdAsync(loyaltyProgramId).ConfigureAwait(true);
    }
}
