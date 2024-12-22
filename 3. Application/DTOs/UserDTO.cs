namespace _3._Application.DTOs;

public class UserDTO
{
    public int UserId { get; set; }

    public required int RoleId { get; set; }

    public required IList<GuestBookingDTO> GuestBookingDTOs { get; set; } = [];

    public required IList<FeedbackDTO> FeedbackDTOs { get; set; } = [];

    public required IList<HousekeepingDTO> HousekeepingDTOs { get; set; } = [];

    public required IList<LoyaltyProgramDTO> LoyaltyProgramDTOs { get; set; } = [];
}
