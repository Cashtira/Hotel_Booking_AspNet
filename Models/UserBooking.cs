using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace MVCmodel.Models
{
    [PrimaryKey(nameof(UserId), nameof(BookingId))]
    [Table(nameof(UserBooking))]
    public class UserBooking
    {
        [Key]
        public required string UserId { get; set; }

        [Key]
        public required int BookingId { get; set; }

        [ForeignKey(nameof(UserId))]
        [InverseProperty(nameof(User.UserBookings))]
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public User User { get; set; } = null!;

        [ForeignKey(nameof(BookingId))]
        [InverseProperty(nameof(Booking.UserBookings))]
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public Booking Booking { get; set; } = null!;
    }
}
