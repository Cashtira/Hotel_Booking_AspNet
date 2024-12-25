namespace _4._Presentation.Controllers;

using _3._Application.DTOs;
using _3._Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public sealed class HousekeepingController(IHousekeepingService housekeepingService) : ControllerBase
{
    private readonly IHousekeepingService housekeepingService = housekeepingService;

    [HttpGet]
    public async Task<IActionResult> GetAllHousekeepingsAsync()
    {
        var genres = await this.housekeepingService.GetAllHousekeepingsAsync().ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
        return HandleResult(genres);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetHousekeepingByIdAsync(int id)
    {
        var genre = await this.housekeepingService.GetHousekeepingByIdAsync(id).ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
        return HandleResult(genre);
    }

    [HttpPost]
    public async Task<IActionResult> AddHousekeepingAsync([FromBody] HousekeepingDTO housekeepingDto)
    {
        if ((!ModelState.IsValid) || (housekeepingDto == null))
        {
            return BadRequest(ModelState);
        }

        await this.housekeepingService.AddHousekeepingAsync(housekeepingDto).ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
        return CreatedAtAction(nameof(GetHousekeepingByIdAsync), new { id = housekeepingDto.HousekeepingId }, housekeepingDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateHousekeepingAsync(int id, [FromBody] HousekeepingDTO housekeepingDto)
    {
        if (housekeepingDto == null)
        {
            return BadRequest();
        }

        if (id != housekeepingDto.HousekeepingId)
        {
            return BadRequest("Invalid ID.");
        }

        await this.housekeepingService.UpdateHousekeepingAsync(housekeepingDto).ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteHousekeepingByIdAsync(int id)
    {
        await this.housekeepingService.DeleteHousekeepingByIdAsync(id).ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
        return NoContent();
    }
}
