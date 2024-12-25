namespace _3._Application.DTOs;

public class UserDTO
{
    // TODO: Khóa chính là gì?
    // Có role hay không?
    public int UserId { get; set; }

    public required string Username { get; set; }

    public required string Password { get; set; }

    public required int RoleId { get; set; }

    public IList<UserBookingDTO> GuestBookingDTOs { get; set; } = [];

    public IList<FeedbackDTO> FeedbackDTOs { get; set; } = [];

    public IList<HousekeepingDTO> HousekeepingDTOs { get; set; } = [];

    public IList<LoyaltyProgramDTO> LoyaltyProgramDTOs { get; set; } = [];
}
