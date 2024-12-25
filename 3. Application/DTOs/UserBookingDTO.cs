namespace _3._Application.DTOs;

public sealed class UserBookingDTO
{
    public required int UserId { get; set; }

    public required int BookingId { get; set; }

    public UserDTO? User { get; set; } = null;

    public BookingDTO? Booking { get; set; } = null;
}
