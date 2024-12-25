namespace _4._Presentation.Controllers;

using _3._Application.DTOs;
using _3._Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public sealed class RoomController(IRoomService roomService) : ControllerBase
{
    private readonly IRoomService roomService = roomService;

    [HttpGet]
    public async Task<IActionResult> GetAllRoomsAsync()
    {
        var rooms = await this.roomService.GetAllRoomsAsync().ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
        return HandleResult(rooms);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetRoomByIdAsync(int id)
    {
        var room = await this.roomService.GetRoomByIdAsync(id).ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
        return HandleResult(room);
    }

    [HttpPost]
    public async Task<IActionResult> AddRoomAsync([FromBody] RoomDTO roomDto)
    {
        if ((!ModelState.IsValid) || (roomDto == null))
        {
            return BadRequest(ModelState);
        }

        await this.roomService.AddRoomAsync(roomDto).ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
        return CreatedAtAction(nameof(GetRoomByIdAsync), new { id = roomDto.RoomId }, roomDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateRoomAsync(int id, [FromBody] RoomDTO roomDto)
    {
        if (roomDto == null)
        {
            return BadRequest();
        }

        if (id != roomDto.RoomId)
        {
            return BadRequest("Invalid ID.");
        }

        await this.roomService.UpdateRoomAsync(roomDto).ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRoomByIdAsync(int id)
    {
        await this.roomService.DeleteRoomByIdAsync(id).ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
        return NoContent();
    }
}
