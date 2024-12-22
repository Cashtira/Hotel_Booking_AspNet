namespace _3._Application.DTOs;

public class HotelDTO
{
    public int HotelId { get; set; }

    public required string Name { get; set; }

    public required string Address { get; set; }

    public required string Phone { get; set; }

    public required string Email { get; set; }

    public required float Rating { get; set; }

    public required TimeOnly CheckinTime { get; set; }

    public required TimeOnly CheckoutTime { get; set; }

    public required IList<UserDTO> UserDTOs { get; set; } = [];

    public required IList<RoomDTO> RoomDTOs { get; set; } = [];

    public required IList<FeedbackDTO> FeedbackDTOs { get; set; } = [];
}

