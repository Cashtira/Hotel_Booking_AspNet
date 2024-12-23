using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2._Domain.Entities
{
    public class ServiceBooking
    {
        public int BookingId { get; set; }
        public int ServiceId { get; set; }
        public decimal Price { get; set; }

        public Booking Booking { get; set; } = null!;
        public Service Service { get; set; } = null!;
    }
}
 