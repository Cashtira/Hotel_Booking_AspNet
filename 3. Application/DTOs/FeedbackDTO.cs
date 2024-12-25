namespace _3._Application.DTOs;

public sealed class FeedbackDTO
{
    public int FeedbackId { get; set; }

    public required string UserId { get; set; }

    public required int HotelId { get; set; }

    public required float Rating { get; set; }

    public string Comment { get; set; } = string.Empty;

    public UserDTO? User { get; set; } = null;

    public HotelDTO? Hotel { get; set; } = null;
}
