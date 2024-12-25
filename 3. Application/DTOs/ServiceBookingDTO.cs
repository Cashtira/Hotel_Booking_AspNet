namespace _3._Application.DTOs;

public sealed class ServiceBookingDTO
{
    public required int BookingId { get; set; }

    public required int ServiceId { get; set; }

    public required decimal Price { get; set; }

    public BookingDTO? Booking { get; set; } = null;

    public ServiceDTO? Service { get; set; } = null;
}
