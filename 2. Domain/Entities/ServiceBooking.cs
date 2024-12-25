namespace _2._Domain.Entities;

using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[PrimaryKey(nameof(BookingId), nameof(ServiceId))]
[Table(nameof(ServiceBooking))]
public sealed class ServiceBooking
{
    [Key]
    public required int BookingId { get; set; }

    [Key]
    public required int ServiceId { get; set; }

    [Range(0, double.MaxValue)]
    [Precision(18, 2)]
    public required decimal Price { get; set; }

    [ForeignKey(nameof(BookingId))]
    [InverseProperty(nameof(Booking.ServiceBookings))]
    [DeleteBehavior(DeleteBehavior.Restrict)]
    public Booking Booking { get; set; } = null!;

    [ForeignKey(nameof(ServiceId))]
    [InverseProperty(nameof(Service.ServiceBookings))]
    [DeleteBehavior(DeleteBehavior.Restrict)]
    public Service Service { get; set; } = null!;
}
