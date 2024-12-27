namespace _4._Presentation.Controllers;

using _3._Application.DTOs;
using _3._Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public sealed class FeedbackController(IFeedbackService feedbackService) : ControllerBase
{
    private readonly IFeedbackService feedbackService = feedbackService;

    [HttpGet]
    [AllowAnonymous]

    public async Task<IActionResult> GetAllFeedbacksAsync()
    {
        var feedbacks = await this.feedbackService.GetAllFeedbacksAsync().ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
        return HandleResult(feedbacks);
    }

    [HttpGet("{id}")]
    [AllowAnonymous]

    public async Task<IActionResult> GetFeedbackByIdAsync(int id)
    {
        var feedback = await this.feedbackService.GetFeedbackByIdAsync(id).ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
        return HandleResult(feedback);
    }

    [HttpPost]
    [AllowAnonymous]

    public async Task<IActionResult> AddFeedbackAsync([FromBody] FeedbackDTO feedbackDto)
    {
        if ((!ModelState.IsValid) || (feedbackDto == null))
        {
            return BadRequest(ModelState);
        }

        await this.feedbackService.AddFeedbackAsync(feedbackDto).ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
        return CreatedAtAction(nameof(GetFeedbackByIdAsync), new { id = feedbackDto.FeedbackId }, feedbackDto);
    }

    [HttpPut("{id}")]
    [AllowAnonymous]

    public async Task<IActionResult> UpdateFeedbackAsync(int id, [FromBody] FeedbackDTO feedbackDto)
    {
        if (feedbackDto == null)
        {
            return BadRequest();
        }

        if (id != feedbackDto.FeedbackId)
        {
            return BadRequest("Invalid ID.");
        }

        await this.feedbackService.UpdateFeedbackAsync(feedbackDto).ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteFeedbackByIdAsync(int id)
    {
        await this.feedbackService.DeleteFeedbackByIdAsync(id).ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
        return NoContent();
    }
}
