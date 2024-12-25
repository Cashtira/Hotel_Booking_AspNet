namespace _2._Domain.Entities;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table(nameof(Feedback))]
public sealed class Feedback
{
    [Key]
    public int FeedbackId { get; set; }

    public required int UserId { get; set; }

    public required int HotelId { get; set; }

    [Range(1, 5)]
    public int Rating { get; set; } = 5;

    [StringLength(200)]
    public string Comment { get; set; } = string.Empty;

    [ForeignKey(nameof(UserId))]
    [InverseProperty(nameof(User.Feedbacks))]
    public User User { get; set; } = null!;

    [ForeignKey(nameof(HotelId))]
    [InverseProperty(nameof(Hotel.Feedbacks))]
    public Hotel Hotel { get; set; } = null!;
}
