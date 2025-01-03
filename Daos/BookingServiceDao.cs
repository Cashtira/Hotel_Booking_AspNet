using QuanLyKhachSan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyKhachSan.Daos
{
    public class BookingServiceDao
    {
        QuanLyKhachSanDBContext myDb = new QuanLyKhachSanDBContext();
        public void Add(BookingService bookingService)
        {
            myDb.BookingServices.Add(bookingService);
            myDb.SaveChanges();
        }
    }
}