namespace _4._Presentation.Controllers;

using _3._Application.DTOs;
using _3._Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public sealed class ServiceController(IServiceService serviceService) : ControllerBase
{
    private readonly IServiceService serviceService = serviceService;

    [HttpGet]
    public async Task<IActionResult> GetAllServicesAsync()
    {
        var genres = await this.serviceService.GetAllServicesAsync().ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
        return HandleResult(genres);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetServiceByIdAsync(int id)
    {
        var genre = await this.serviceService.GetServiceByIdAsync(id).ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
        return HandleResult(genre);
    }

    [HttpPost]
    public async Task<IActionResult> AddServiceAsync([FromBody] ServiceDTO serviceDto)
    {
        if ((!ModelState.IsValid) || (serviceDto == null))
        {
            return BadRequest(ModelState);
        }

        await this.serviceService.AddServiceAsync(serviceDto).ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
        return CreatedAtAction(nameof(GetServiceByIdAsync), new { id = serviceDto.ServiceId }, serviceDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateServiceAsync(int id, [FromBody] ServiceDTO serviceDto)
    {
        if (serviceDto == null)
        {
            return BadRequest();
        }

        if (id != serviceDto.ServiceId)
        {
            return BadRequest("Invalid ID.");
        }

        await this.serviceService.UpdateServiceAsync(serviceDto).ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteServiceByIdAsync(int id)
    {
        await this.serviceService.DeleteServiceByIdAsync(id).ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
        return NoContent();
    }
}
