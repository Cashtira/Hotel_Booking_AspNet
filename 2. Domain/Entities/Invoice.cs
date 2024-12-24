namespace _2._Domain.Entities;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using _2._Domain.Enums;

[Table(nameof(Invoice))]
public sealed class Invoice
{
    [Key]
    public int InvoiceId { get; set; }

    public required DateTime InvoiceTime { get; set; }

    public required int BookingId { get; set; }

    public required PaymentMethod PaymentMethod { get; set; } = PaymentMethod.Cash;

    [ForeignKey(nameof(BookingId))]
    [InverseProperty(nameof(Booking.Invoices))]
    public required Booking Booking { get; set; } = null!;
}
