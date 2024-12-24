namespace _2._Domain.Entities;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using _2._Domain.Enums;

[Table(nameof(Booking))]
public sealed class Booking
{
    [Key]
    public int BookingId { get; set; }

    public required DateTime BookingTime { get; set; }

    public required int UserId { get; set; }

    public required DateTime CheckinDate { get; set; }

    public required DateTime CheckoutDate { get; set; }

    public required BookingStatus Status { get; set; } = BookingStatus.Pending;

    [InverseProperty(nameof(User.Bookings))]
    public required User User { get; set; } = null!;

    [InverseProperty(nameof(RoomBooking.Booking))]
    public required ICollection<RoomBooking> RoomBookings { get; set; } = [];

    [InverseProperty(nameof(ServiceBooking.Booking))]
    public required ICollection<ServiceBooking> ServiceBookings { get; set; } = [];

    [InverseProperty(nameof(UserBooking.Booking))]
    public required ICollection<UserBooking> UserBookings { get; set; } = [];

    [InverseProperty(nameof(Invoice.Booking))]
    public required ICollection<Invoice> Invoices { get; set; } = [];
}
