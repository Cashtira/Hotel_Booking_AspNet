namespace _4._Presentation.Controllers;

using _3._Application.DTOs;
using _3._Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public sealed class MaintenanceController(IMaintenanceService maintenanceService) : ControllerBase
{
    private readonly IMaintenanceService maintenanceService = maintenanceService;

    [HttpGet]
    public async Task<IActionResult> GetAllMaintenancesAsync()
    {
        var maintenances = await this.maintenanceService.GetAllMaintenancesAsync().ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
        return HandleResult(maintenances);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetMaintenanceByIdAsync(int id)
    {
        var maintenance = await this.maintenanceService.GetMaintenanceByIdAsync(id).ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
        return HandleResult(maintenance);
    }

    [HttpPost]
    public async Task<IActionResult> AddMaintenanceAsync([FromBody] MaintenanceDTO maintenanceDto)
    {
        if ((!ModelState.IsValid) || (maintenanceDto == null))
        {
            return BadRequest(ModelState);
        }

        await this.maintenanceService.AddMaintenanceAsync(maintenanceDto).ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
        return CreatedAtAction(nameof(GetMaintenanceByIdAsync), new { id = maintenanceDto.MaintenanceId }, maintenanceDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateMaintenanceAsync(int id, [FromBody] MaintenanceDTO maintenanceDto)
    {
        if (maintenanceDto == null)
        {
            return BadRequest();
        }

        if (id != maintenanceDto.MaintenanceId)
        {
            return BadRequest("Invalid ID.");
        }

        await this.maintenanceService.UpdateMaintenanceAsync(maintenanceDto).ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMaintenanceByIdAsync(int id)
    {
        await this.maintenanceService.DeleteMaintenanceByIdAsync(id).ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
        return NoContent();
    }
}
