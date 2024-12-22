namespace _3._Application.DTOs;

public class ServiceBookingDTO
{
    public required int BookingId { get; set; }

    public required int ServiceId { get; set; }

    public required decimal Price { get; set; }
}
