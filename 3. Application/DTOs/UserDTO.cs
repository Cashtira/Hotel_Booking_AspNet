namespace _3._Application.DTOs;

public class UserDTO
{
    // TODO: Khóa chính là gì?
    // Có role hay không?
    public string Id { get; set; }

    public required string UserName { get; set; }

    public required IList<string> Roles { get; set; } = [];

    public IList<UserBookingDTO> GuestBookingDTOs { get; set; } = [];

    public IList<FeedbackDTO> FeedbackDTOs { get; set; } = [];

    public IList<HousekeepingDTO> HousekeepingDTOs { get; set; } = [];

    public IList<LoyaltyProgramDTO> LoyaltyProgramDTOs { get; set; } = [];
}
