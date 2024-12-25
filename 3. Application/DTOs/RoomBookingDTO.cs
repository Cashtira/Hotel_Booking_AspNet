namespace _3._Application.DTOs;

public sealed class RoomBookingDTO
{
    public required int BookingId { get; set; }

    public required int RoomId { get; set; }

    public required decimal Price { get; set; }

    public BookingDTO? Booking { get; set; } = null;

    public RoomDTO? Room { get; set; } = null;
}
