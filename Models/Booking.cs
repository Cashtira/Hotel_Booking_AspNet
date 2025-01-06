using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace QuanLyKhachSan.Models
{
    public class Booking
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idBooking { get; set; }

        public int totalMoney { get; set; }

        public string checkInDate { get; set; }

        public string checkOutDate { get; set; }

        public int status { get; set; }
        public bool isPayment { get; set; }

        public DateTime createdDate { get; set; }

        public int idRoom { get; set; }

        public int idUser { get; set; }

        public virtual Room Room { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<BookingService> BookingServices { get; set; }
    }
}