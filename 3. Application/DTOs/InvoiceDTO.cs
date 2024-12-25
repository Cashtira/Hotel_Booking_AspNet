using _2._Domain.Enums;

namespace _3._Application.DTOs;

public sealed class InvoiceDTO
{
    public int InvoiceId { get; set; }

    public required DateTimeOffset InvoiceTime { get; set; }

    public required int BookingId { get; set; }

    public required PaymentMethod PaymentMethod { get; set; }

    public BookingDTO Booking { get; set; } = null!;
}
