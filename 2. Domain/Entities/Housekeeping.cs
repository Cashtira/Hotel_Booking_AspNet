namespace _2._Domain.Entities;

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

[Table(nameof(Housekeeping))]
public sealed class Housekeeping
{
    [Key]
    public int HousekeepingId { get; set; }

    public required int RoomId { get; set; }

    public required int UserId { get; set; }

    [StringLength(200)]
    public required string IssueDescription { get; set; }

    public required DateTimeOffset? CleanTime { get; set; } = null;

    [ForeignKey(nameof(RoomId))]
    [InverseProperty(nameof(Room.Housekeepings))]
    public required Room Room { get; set; } = null!;

    [ForeignKey(nameof(UserId))]
    [InverseProperty(nameof(User.Housekeepings))]
    public required User User { get; set; } = null!;
}
