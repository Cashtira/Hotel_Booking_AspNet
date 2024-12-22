namespace _3._Application.DTOs;

public class InvoiceDTO
{
    public int InvoiceId { get; set; }

    public required DateTimeOffset InvoiceTime { get; set; }

    public required int BookingId { get; set; }

    public required int PaymentMethod { get; set; }
}
