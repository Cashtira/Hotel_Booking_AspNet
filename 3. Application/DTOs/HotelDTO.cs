namespace _3._Application.DTOs;

public sealed class HotelDTO
{
    public int HotelId { get; set; }

    public required string Name { get; set; }

    public required string Address { get; set; }

    public required string Phone { get; set; }

    public required string Email { get; set; }

    public float? Rating { get; set; } = null;

    public TimeOnly CheckinTime { get; set; } = new TimeOnly(14, 0, 0);

    public TimeOnly CheckoutTime { get; set; } = new TimeOnly(12, 0, 0);

    public IList<UserDTO> UserDTOs { get; set; } = [];

    public IList<RoomDTO> RoomDTOs { get; set; } = [];

    public IList<FeedbackDTO> FeedbackDTOs { get; set; } = [];
}

