using _2._Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2._Domain.Entities
{
    public class Booking
    {
        public int BookingId { get; set; }
        public DateTime BookingTime { get; set; }
        public required DateTime CheckinDate { get; set; }
        public required DateTime CheckoutDate { get; set; }
        public BookingStatus Status { get; set; } = BookingStatus.Pending;
        public required int RoomId { get; set; }
        public required int UserId { get; set; }



        //mock
        public Room Room { get; set; } = null!;
        public User User { get; set; } = null!;
        public required ICollection<Invoice> Invoices { get; set; } = [];
        public required ICollection<RoomBooking> RoomBookings { get; set; } = [];
        public required ICollection<ServiceBooking> ServiceBookings { get; set; } = [];
        public required ICollection<UserBooking> UserBookings { get; set;} = [];
    }
}
