namespace _3._Application.Interfaces.Services;

using _3._Application.DTOs;

public interface IFeedbackService
{
    Task<List<FeedbackDTO>> GetAllFeedbacksAsync();

    Task<FeedbackDTO?> GetFeedbackByIdAsync(int feedbackId);

    Task AddFeedbackAsync(FeedbackDTO feedbackDto);

    Task UpdateFeedbackAsync(FeedbackDTO feedbackDto);

    Task DeleteFeedbackByIdAsync(int feedbackId);
}
