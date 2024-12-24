namespace _3._Application.Interfaces.Services;

using _3._Application.DTOs;

public interface ILoyaltyProgramService
{
    Task<List<LoyaltyProgramDTO>> GetAllLoyaltyProgramsAsync();

    Task<LoyaltyProgramDTO?> GetLoyaltyProgramByIdAsync(int loyaltyProgramId);

    Task AddLoyaltyProgramAsync(LoyaltyProgramDTO loyaltyProgramDto);

    Task UpdateLoyaltyProgramAsync(LoyaltyProgramDTO loyaltyProgramDto);

    Task DeleteLoyaltyProgramByIdAsync(int loyaltyProgramId);
}
