namespace _2._Domain.Interfaces.Repositories;

using _2._Domain.Entities;

public interface ILoyaltyProgramRepository
{
    Task<IEnumerable<LoyaltyProgram>> GetAllLoyaltyProgramsAsync();

    Task<LoyaltyProgram?> GetLoyaltyProgramByIdAsync(int loyaltyProgramId);

    Task AddLoyaltyProgramAsync(LoyaltyProgram loyaltyProgram);

    Task UpdateLoyaltyProgramAsync(LoyaltyProgram loyaltyProgram);

    Task DeleteLoyaltyProgramByIdAsync(int loyaltyProgramId);
}
