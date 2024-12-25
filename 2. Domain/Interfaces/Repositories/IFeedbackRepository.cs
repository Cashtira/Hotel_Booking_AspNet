namespace _2._Domain.Interfaces.Repositories;

using _2._Domain.Entities;

public interface IFeedbackRepository
{
    Task<IEnumerable<Feedback>> GetAllFeedbacksAsync();

    Task<Feedback?> GetFeedbackByIdAsync(int feedbackId);

    Task AddFeedbackAsync(Feedback feedback);

    Task UpdateFeedbackAsync(Feedback feedback);

    Task DeleteFeedbackByIdAsync(int feedbackId);
}
