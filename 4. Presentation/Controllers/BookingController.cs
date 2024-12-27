namespace _4._Presentation.Controllers;

using _3._Application.DTOs;
using _3._Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public sealed class BookingController(IBookingService bookingService) : ControllerBase
{
    private readonly IBookingService bookingService = bookingService;

    [HttpGet]
    [AllowAnonymous]

    public async Task<IActionResult> GetAllBookingsAsync()
    {
        var bookings = await this.bookingService.GetAllBookingsAsync().ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
        return HandleResult(bookings);
    }

    [HttpGet("{id}")]
    [AllowAnonymous]

    public async Task<IActionResult> GetBookingByIdAsync(int id)
    {
        var booking = await this.bookingService.GetBookingByIdAsync(id).ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
        return HandleResult(booking);
    }

    [HttpPost]
    [AllowAnonymous]

    public async Task<IActionResult> AddBookingAsync([FromBody] BookingDTO bookingDto)
    {
        if ((!ModelState.IsValid) || (bookingDto == null))
        {
            return BadRequest(ModelState);
        }

        await this.bookingService.AddBookingAsync(bookingDto).ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
        return CreatedAtAction(nameof(GetBookingByIdAsync), new { id = bookingDto.BookingId }, bookingDto);
    }

    [HttpPut("{id}")]
    [AllowAnonymous]

    public async Task<IActionResult> UpdateBookingAsync(int id, [FromBody] BookingDTO bookingDto)
    {
        if (bookingDto == null)
        {
            return BadRequest();
        }

        if (id != bookingDto.BookingId)
        {
            return BadRequest("Invalid ID.");
        }

        await this.bookingService.UpdateBookingAsync(bookingDto).ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBookingByIdAsync(int id)
    {
        await this.bookingService.DeleteBookingByIdAsync(id).ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
        return NoContent();
    }
}
