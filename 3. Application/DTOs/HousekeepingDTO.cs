namespace _3._Application.DTOs;

public sealed class HousekeepingDTO
{
    public int HousekeepingId { get; set; }

    public required int RoomId { get; set; }

    public required string UserId { get; set; }

    public DateTimeOffset? CleanDate { get; set; } = null;

    public required string IssueDescription { get; set; }

    public RoomDTO? Room { get; set; } = null;

    public UserDTO? User { get; set; } = null;
}
