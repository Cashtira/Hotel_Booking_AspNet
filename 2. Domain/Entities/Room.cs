namespace _2._Domain.Entities;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using _2._Domain.Enums;

[Table(nameof(Room))]
public sealed class Room
{
    [Key]
    public int RoomId { get; set; }

    [StringLength(50)]
    public required string Name { get; set; }

    public required int HotelId { get; set; }

    public required int RoomTypeId { get; set; }

    public RoomStatus Status { get; set; } = RoomStatus.Available;

    [ForeignKey(nameof(HotelId))]
    [InverseProperty(nameof(Hotel.Rooms))]
    public Hotel Hotel { get; set; } = null!;

    [ForeignKey(nameof(RoomTypeId))]
    [InverseProperty(nameof(RoomType.Rooms))]
    public RoomType RoomType { get; set; } = null!;

    [InverseProperty(nameof(Housekeeping.Room))]
    public ICollection<Housekeeping> Housekeepings { get; set; } = [];

    [InverseProperty(nameof(Maintenance.Room))]
    public ICollection<Maintenance> Maintenances { get; set; } = [];

    [InverseProperty(nameof(RoomBooking.Room))]
    public ICollection<RoomBooking> RoomBookings { get; set; } = [];
}
