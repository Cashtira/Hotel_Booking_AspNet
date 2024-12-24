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
    public required int Rating { get; set; }

    [StringLength(200)]
    public required string Comment { get; set; }

    [ForeignKey(nameof(UserId))]
    [InverseProperty(nameof(User.Feedbacks))]
    public required User User { get; set; } = null!;

    [ForeignKey(nameof(HotelId))]
    [InverseProperty(nameof(Hotel.Feedbacks))]
    public required Hotel Hotel { get; set; } = null!;
}
