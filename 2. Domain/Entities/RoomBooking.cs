namespace _2._Domain.Entities;

using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[PrimaryKey(nameof(BookingId), nameof(RoomId))]
[Table(nameof(RoomBooking))]
public sealed class RoomBooking
{
    [Key]
    public required int BookingId { get; set; }

    [Key]
    public required int RoomId { get; set; }

    [Range(0, double.MaxValue)]
    [Precision(18, 2)]
    public required decimal Price { get; set; }

    [ForeignKey(nameof(BookingId))]
    [InverseProperty(nameof(Booking.RoomBookings))]
    public Booking Booking { get; set; } = null!;

    [ForeignKey(nameof(RoomId))]
    [InverseProperty(nameof(Booking.RoomBookings))]
    public Room Room { get; set; } = null!;
}
