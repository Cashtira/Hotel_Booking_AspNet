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

    public PaymentMethod PaymentMethod { get; set; }

    [ForeignKey(nameof(BookingId))]
    [InverseProperty(nameof(Booking.Invoices))]
    public Booking Booking { get; set; } = null!;
}
