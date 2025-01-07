using QuanLyKhachSan.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace QuanLyKhachSan.Repositories
{
    public class BookingServiceRepository
    {
        private readonly QuanLyKhachSanDBContext _context;
        public BookingServiceRepository(QuanLyKhachSanDBContext context)
        {
            _context = context;
        }
        public async Task AddBookingServiceAsync(BookingService bookingService)
        {
            _context.BookingServices.Add(bookingService);
            await _context.SaveChangesAsync();
        }
        public async Task<List<Service>> GetServicesByBookingIdAsync(int bookingId)
        {
            return await _context.BookingServices
                .Where(bs => bs.idBooking == bookingId)  // Lọc theo BookingId
                .Select(bs => bs.Service)  // Lấy các dịch vụ liên quan
                .ToListAsync();
        }

    }
}