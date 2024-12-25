namespace _4._Presentation.Controllers;

using _3._Application.DTOs;
using _3._Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public sealed class RoomTypeController(IRoomTypeService roomTypeService) : ControllerBase
{
    private readonly IRoomTypeService roomTypeService = roomTypeService;

    [HttpGet]
    public async Task<IActionResult> GetAllRoomTypesAsync()
    {
        var genres = await this.roomTypeService.GetAllRoomTypesAsync().ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
        return HandleResult(genres);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetRoomTypeByIdAsync(int id)
    {
        var genre = await this.roomTypeService.GetRoomTypeByIdAsync(id).ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
        return HandleResult(genre);
    }

    [HttpPost]
    public async Task<IActionResult> AddRoomTypeAsync([FromBody] RoomTypeDTO roomTypeDto)
    {
        if ((!ModelState.IsValid) || (roomTypeDto == null))
        {
            return BadRequest(ModelState);
        }

        await this.roomTypeService.AddRoomTypeAsync(roomTypeDto).ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
        return CreatedAtAction(nameof(GetRoomTypeByIdAsync), new { id = roomTypeDto.RoomTypeId }, roomTypeDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateRoomTypeAsync(int id, [FromBody] RoomTypeDTO roomTypeDto)
    {
        if (roomTypeDto == null)
        {
            return BadRequest();
        }

        if (id != roomTypeDto.RoomTypeId)
        {
            return BadRequest("Invalid ID.");
        }

        await this.roomTypeService.UpdateRoomTypeAsync(roomTypeDto).ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRoomTypeByIdAsync(int id)
    {
        await this.roomTypeService.DeleteRoomTypeByIdAsync(id).ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
        return NoContent();
    }
}
