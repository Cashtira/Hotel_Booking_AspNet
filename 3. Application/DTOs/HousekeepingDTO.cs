namespace _3._Application.DTOs;

public class HousekeepingDTO
{
    public int HousekeepingId { get; set; }

    public required int RoomId { get; set; }

    public required int StaffId { get; set; }

    public DateTimeOffset? CleanDate { get; set; }

    public required string IssueDescription { get; set; }
}
