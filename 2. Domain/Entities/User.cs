namespace _2._Domain.Entities;

using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table(nameof(User))]
public sealed class User : IdentityUser<int>
{
    [Key]
    public int UserId { get; set; }


    [MinLength(5)]
    [StringLength(50)]
    public required string FullName { get; set; }

    public DateTime? DateOfBirth { get; set; } = null;

    [InverseProperty(nameof(Booking.User))]
    public required ICollection<Booking> Bookings { get; set; } = [];

    [InverseProperty(nameof(UserBooking.User))]
    public required ICollection<UserBooking> UserBookings { get; set; } = [];

    [InverseProperty(nameof(Feedback.User))]
    public required ICollection<Feedback> Feedbacks { get; set; } = [];

    [InverseProperty(nameof(Feedback.User))]
    public required ICollection<Housekeeping> Housekeepings { get; set; } = [];

    [InverseProperty(nameof(LoyaltyProgram.User))]
    public required ICollection<LoyaltyProgram> LoyaltyPrograms { get; set; } = [];
}
