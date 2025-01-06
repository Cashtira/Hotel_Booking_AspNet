using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace QuanLyKhachSan.Models
{
    public class BookingService
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idBookingService { get; set; }

        public int idBooking { get; set; }

        public int idService { get; set; }

        public virtual Booking Booking { get; set; }

        public virtual Service Service { get; set; }

    }
}