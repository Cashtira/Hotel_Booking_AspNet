using QuanLyKhachSan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace QuanLyKhachSan.Repositories
{
    public class BookingServiceRepository
    {
        private  readonly QuanLyKhachSanDBContext _context = new QuanLyKhachSanDBContext();
        public async Task AddBookingService(BookingService bookingService)
        {
            _context.BookingServices.Add(bookingService);
            await _context.SaveChangesAsync();
        }
    }
}