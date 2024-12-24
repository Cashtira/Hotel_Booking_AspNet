namespace _2._Domain.Entities;

using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

[PrimaryKey(nameof(UserId), nameof(BookingId))]
[Table(nameof(UserBooking))]
public class UserBooking
{
    [Key]
    public required int UserId { get; set; }

    [Key]
    public required int BookingId { get; set; }

    [ForeignKey(nameof(UserId))]
    [InverseProperty(nameof(User.UserBookings))]
    public required User User { get; set; } = null!;

    [ForeignKey(nameof(BookingId))]
    [InverseProperty(nameof(Booking.UserBookings))]
    public required Booking Booking { get; set; } = null!;
}
