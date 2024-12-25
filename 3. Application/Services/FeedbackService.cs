namespace _3._Application.Services;

using _2._Domain.Entities;
using _2._Domain.Interfaces.Repositories;
using _3._Application.DTOs;
using _3._Application.Interfaces.Services;
using AutoMapper;

public class FeedbackService(IFeedbackRepository feedbackRepository, IMapper mapper) : IFeedbackService
{
    private readonly IFeedbackRepository feedbackRepository = feedbackRepository;
    private readonly IMapper mapper = mapper;

    public async Task<List<FeedbackDTO>> GetAllFeedbacksAsync()
    {
        var feedbacks = await this.feedbackRepository.GetAllFeedbacksAsync().ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
        return this.mapper.Map<List<FeedbackDTO>>(feedbacks);
    }

    public async Task<FeedbackDTO?> GetFeedbackByIdAsync(int feedbackId)
    {
        var feedback = await this.feedbackRepository.GetFeedbackByIdAsync(feedbackId).ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
        return this.mapper.Map<FeedbackDTO>(feedback);
    }

    public async Task AddFeedbackAsync(FeedbackDTO feedbackDto)
    {
        var feedback = this.mapper.Map<Feedback>(feedbackDto);
        await this.feedbackRepository.AddFeedbackAsync(feedback).ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
    }

    public async Task UpdateFeedbackAsync(FeedbackDTO feedbackDto)
    {
        var feedback = this.mapper.Map<Feedback>(feedbackDto);
        await this.feedbackRepository.UpdateFeedbackAsync(feedback).ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
    }

    public async Task DeleteFeedbackByIdAsync(int feedbackId)
    {
        await this.feedbackRepository.DeleteFeedbackByIdAsync(feedbackId).ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
    }
}
