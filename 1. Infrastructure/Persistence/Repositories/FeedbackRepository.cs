namespace _1._Infrastructure.Persistence.Repositories;

using System.Collections.Generic;
using System.Threading.Tasks;
using _2._Domain.Entities;
using _2._Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

public class FeedbackRepository(ApplicationDbContext dbContext) : IFeedbackRepository
{
    private readonly ApplicationDbContext dbContext = dbContext ?? new ApplicationDbContext();

    public async Task<IEnumerable<Feedback>> GetAllFeedbacksAsync()
    {
        return await this.dbContext.Feedbacks.ToListAsync().ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
    }

    public async Task<Feedback?> GetFeedbackByIdAsync(int feedbackId)
    {
        return await this.dbContext.Feedbacks.FirstOrDefaultAsync(e => e.FeedbackId == feedbackId).ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
    }

    public async Task AddFeedbackAsync(Feedback feedback)
    {
        this.dbContext.Add(feedback);
        await this.dbContext.SaveChangesAsync().ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
    }
    public async Task UpdateFeedbackAsync(Feedback feedback)
    {
        this.dbContext.Update(feedback);
        await this.dbContext.SaveChangesAsync().ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
    }

    public async Task DeleteFeedbackByIdAsync(int feedbackId)
    {
        var feedback = await this.GetFeedbackByIdAsync(feedbackId).ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
        if (feedback != null)
        {
            this.dbContext.Remove(feedback);
            await this.dbContext.SaveChangesAsync().ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
        }
    }
}
