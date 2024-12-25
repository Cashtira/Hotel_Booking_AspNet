namespace _2._Domain.Entities;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

[Table(nameof(Service))]
public class Service
{
    [Key]
    public int ServiceId { get; set; }

    [MaxLength(50)]
    public required string Name { get; set; }

    [Range(0, double.MaxValue)]
    [Precision(18, 2)]
    public required decimal Price { get; set; }

    [MaxLength(200)]
    public string Description { get; set; } = string.Empty;

    [InverseProperty(nameof(ServiceBooking.Service))]
    public ICollection<ServiceBooking> ServiceBookings { get; set; } = [];
}
