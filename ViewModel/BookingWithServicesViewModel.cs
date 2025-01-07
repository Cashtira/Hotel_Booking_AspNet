using QuanLyKhachSan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyKhachSan.ViewModel
{
    public class BookingWithServicesViewModel
    {
        public Booking Booking { get; set; }
        public List<Service> Services { get; set; }
    }
}