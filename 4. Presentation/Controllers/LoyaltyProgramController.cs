namespace _4._Presentation.Controllers;

using _3._Application.DTOs;
using _3._Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public sealed class LoyaltyProgramController(ILoyaltyProgramService loyaltyProgramService) : ControllerBase
{
    private readonly ILoyaltyProgramService loyaltyProgramService = loyaltyProgramService;

    [HttpGet]
    public async Task<IActionResult> GetAllLoyaltyProgramsAsync()
    {
        var loyaltyPrograms = await this.loyaltyProgramService.GetAllLoyaltyProgramsAsync().ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
        return HandleResult(loyaltyPrograms);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetLoyaltyProgramByIdAsync(int id)
    {
        var loyaltyProgram = await this.loyaltyProgramService.GetLoyaltyProgramByIdAsync(id).ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
        return HandleResult(loyaltyProgram);
    }

    [HttpPost]
    public async Task<IActionResult> AddLoyaltyProgramAsync([FromBody] LoyaltyProgramDTO loyaltyProgramDto)
    {
        if ((!ModelState.IsValid) || (loyaltyProgramDto == null))
        {
            return BadRequest(ModelState);
        }

        await this.loyaltyProgramService.AddLoyaltyProgramAsync(loyaltyProgramDto).ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
        return CreatedAtAction(nameof(GetLoyaltyProgramByIdAsync), new { id = loyaltyProgramDto.LoyaltyProgramId }, loyaltyProgramDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateLoyaltyProgramAsync(int id, [FromBody] LoyaltyProgramDTO loyaltyProgramDto)
    {
        if (loyaltyProgramDto == null)
        {
            return BadRequest();
        }

        if (id != loyaltyProgramDto.LoyaltyProgramId)
        {
            return BadRequest("Invalid ID.");
        }

        await this.loyaltyProgramService.UpdateLoyaltyProgramAsync(loyaltyProgramDto).ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLoyaltyProgramByIdAsync(int id)
    {
        await this.loyaltyProgramService.DeleteLoyaltyProgramByIdAsync(id).ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
        return NoContent();
    }
}
