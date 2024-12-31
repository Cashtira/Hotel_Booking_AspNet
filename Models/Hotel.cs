using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCmodel.Models
{
    [Table(nameof(Hotel))]
    public sealed class Hotel
    {
        [Key]
        public int HotelId { get; set; }

        [StringLength(50)]
        public required string Name { get; set; }

        [StringLength(200)]
        public required string Address { get; set; }

        [StringLength(50)]
        [Unicode(false)]
        public required string Phone { get; set; }

        [StringLength(50)]
        [Unicode(false)]
        public required string Email { get; set; }

        [Range(1.0F, 5.0F)]
        public float? Rating { get; set; } = null;

        public TimeOnly CheckinTime { get; set; } = new TimeOnly(14, 0, 0);

        public TimeOnly CheckoutTime { get; set; } = new TimeOnly(12, 0, 0);

        [InverseProperty(nameof(Feedback.Hotel))]
        public ICollection<Feedback> Feedbacks { get; set; } = [];

        [InverseProperty(nameof(Room.Hotel))]
        public ICollection<Room> Rooms { get; set; } = [];
    }
}
