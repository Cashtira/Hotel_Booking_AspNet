using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2._Domain.Entities
{
    public class Service
    {
        public int ServiceId { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required decimal Price { get; set; }

        public ICollection<ServiceBooking> ServiceBookings { get; set; } = new List<ServiceBooking>();
    }
}
