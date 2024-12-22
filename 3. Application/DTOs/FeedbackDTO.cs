namespace _3._Application.DTOs;

public class FeedbackDTO
{
    public int FeedbackId { get; set; }

    public required int GuestId { get; set; }

    public required int HotelId { get; set; }

    public required float Rating { get; set; }

    public required string Comment { get; set; }
}
