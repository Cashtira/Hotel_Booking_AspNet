using QuanLyKhachSan.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Web;

namespace QuanLyKhachSan.Repositories
{
    public class BookingRepository
    {
        private readonly QuanLyKhachSanDBContext _context;
        public BookingRepository(QuanLyKhachSanDBContext context)
        {
            _context = context;
        }

        public async Task AddBookingAsync(Booking booking)
        {
            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();
        }
        public async Task<Booking> GetBookingByIdAsync(int id)
        {
            return await _context.Bookings.FirstOrDefaultAsync(x => x.idBooking == id);
        }
        public async Task<List<Booking>> GetAllBookingAsync()
        {
            return await _context.Bookings.OrderByDescending(x=>x.createdDate).ToListAsync();
        }
        public async Task<List<BookingService>> GetBookingServicesAsync(int bookingId)
        {
            return await _context.BookingServices.Where(x => x.idBooking == bookingId).ToListAsync();
        }

        public async Task UpdateBookingAsync(Booking booking)
        {
            var obj = await _context.Bookings.FirstOrDefaultAsync(x => x.idBooking == booking.idBooking);
            if (obj == null)
            {
                throw new Exception("Booking not found");
            }

            obj.status = booking.status;
            obj.isPayment = booking.isPayment;
            await _context.SaveChangesAsync();
        }


        public async Task<Booking> GetBookingByRoomIdAsync(int roomId)
        {
            return await _context.Bookings.FirstOrDefaultAsync(x => x.idRoom == roomId && x.status != 2);
        }

        public async Task<List<Booking>> GetBookingsByRoomIdAndStatusAsync(int roomId, params int[] statuses)
        {
            return await _context.Bookings
                .Where(x => x.idRoom == roomId && statuses.Contains(x.status))
                .ToListAsync();
        }

        public async Task<List<Booking>> GetBookingsByIdUserAsync(int idUser)
        {
            return await  _context.Bookings.Where(x => x.idUser == idUser).ToListAsync();
        }

       
        public async Task<int> GetTotalMoneyForMonthAsync(int month)
        {
            return await _context.Bookings
                 .Where(x => x.createdDate.Month == month && x.isPayment)
                 .SumAsync(x => (int?)x.totalMoney) ?? 0;
        }
        public async Task<bool> HasUserBookedRoomAsync(int userId, int roomId)
        {
            var bookings = await _context.Bookings
                .Where(x => x.idUser == userId && x.idRoom == roomId && x.status == 3)
                .ToListAsync();
            return bookings.Any();
        }
        public async Task<List<Booking>> GetBookedRoomAsync(int roomId)
        {
            return await _context.Bookings
                                 .Where(b => b.idRoom == roomId && (b.status == 0 || b.status == 1)) // Lọc trạng thái "đang chờ" hoặc "đã đặt"
                                 .ToListAsync();
        }


        //Booking
        public async Task CreateBookingAsync(Booking booking, User user, int numberBooking, Room room, int priceService, int[] idService)
        {
            // Cấu hình thông tin booking
            booking.idUser = user.idUser;
            booking.createdDate = DateTime.Now;
            booking.isPayment = false;
            booking.status = 0;
            booking.totalMoney = (room.cost * numberBooking - room.cost * numberBooking * room.discount / 100) + priceService;

            // Thêm booking vào cơ sở dữ liệu
            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            // Thêm danh sách dịch vụ
            if (idService != null)
            {
                var bookingServices = idService.Select(serviceId => new BookingService
                {
                    idService = serviceId,
                    idBooking = booking.idBooking
                }).ToList();

                _context.BookingServices.AddRange(bookingServices);
                await _context.SaveChangesAsync();
            }
        }

    }
}