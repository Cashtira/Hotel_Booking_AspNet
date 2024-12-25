namespace _2._Domain.Entities;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

[Table(nameof(RoomType))]
public class RoomType
{
    [Key]
    public int RoomTypeId { get; set; }

    [StringLength(50)]
    public required string Name { get; set; }

    [Range(0, int.MaxValue)]
    public required int Capacity { get; set; }

    [Range(0, double.MaxValue)]
    [Precision(18, 2)]
    public required decimal PricePerNight { get; set; }

    [Range(0, double.MaxValue)]
    [Precision(18, 2)]
    public required decimal PricePerHour { get; set; }

    [StringLength(200)]
    public string Description { get; set; } = string.Empty;

    [InverseProperty(nameof(Room.RoomType))]
    public ICollection<Room> Rooms { get; set; } = [];
}
