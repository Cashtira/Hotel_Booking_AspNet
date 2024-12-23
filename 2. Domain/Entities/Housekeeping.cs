using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2._Domain.Entities
{
    public class Housekeeping
    {
        public int HotelId { get; set; }
        public required string Name { get; set; }
        public required string Address { get; set; }
        public required string Phone { get; set; }
        public required string Email { get; set; }
        public float Rating { get; set; }
        public TimeOnly CheckinTime { get; set; }
        public TimeOnly CheckoutTime { get; set; }

        public ICollection<Room> Rooms { get; set; } = new List<Room>();
        public ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();
    }
}
