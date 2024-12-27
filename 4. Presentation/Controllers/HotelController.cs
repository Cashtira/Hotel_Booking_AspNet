namespace _4._Presentation.Controllers;

using _3._Application.DTOs;
using _3._Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public sealed class HotelController(IHotelService hotelService) : ControllerBase
{
    private readonly IHotelService hotelService = hotelService;

    [HttpGet]
    [Authorize(Roles = "Admin, Manager, User")]

    public async Task<IActionResult> GetAllHotelsAsync()
    {
        var hotels = await this.hotelService.GetAllHotelsAsync().ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
        return HandleResult(hotels);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetHotelByIdAsync(int id)
    {
        var hotel = await this.hotelService.GetHotelByIdAsync(id).ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
        return HandleResult(hotel);
    }

    [HttpPost]
    public async Task<IActionResult> AddHotelAsync([FromBody] HotelDTO hotelDto)
    {
        if ((!ModelState.IsValid) || (hotelDto == null))
        {
            return BadRequest(ModelState);
        }

        await this.hotelService.AddHotelAsync(hotelDto).ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
        return CreatedAtAction(nameof(GetHotelByIdAsync), new { id = hotelDto.HotelId }, hotelDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateHotelAsync(int id, [FromBody] HotelDTO hotelDto)
    {
        if (hotelDto == null)
        {
            return BadRequest();
        }

        if (id != hotelDto.HotelId)
        {
            return BadRequest("Invalid ID.");
        }

        await this.hotelService.UpdateHotelAsync(hotelDto).ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteHotelByIdAsync(int id)
    {
        await this.hotelService.DeleteHotelByIdAsync(id).ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
        return NoContent();
    }
}
