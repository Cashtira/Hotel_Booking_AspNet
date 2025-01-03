using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCmodel.Models;

namespace MVCmodel.Controllers
{
    public class ManagerController : Controller
    {
        private readonly HotelManagementContext _context;

        public ManagerController(HotelManagementContext context)
        {
            _context = context;
        }

        // GET: Manager/Index
        public async Task<IActionResult> Index()
        {
            // Lấy danh sách đặt phòng
            var bookings = await _context.Bookings
                .Include(b => b.Guest)
                .Include(b => b.Room)
                .ToListAsync();

            // Lấy danh sách khách hàng
            ViewBag.Guests = await _context.Guests.ToListAsync();

            // Lấy danh sách phòng
            ViewBag.Rooms = await _context.Rooms.ToListAsync();

            // Lấy danh sách dịch vụ
            ViewBag.Services = await _context.Services.ToListAsync();

            return View(bookings);
        }

        // POST: Manager/BookRoom
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BookRoom(int GuestID, int RoomID, DateTime CheckinDate, DateTime CheckoutDate)
        {
            if (CheckinDate >= CheckoutDate)
            {
                TempData["Error"] = "Ngày nhận phòng phải nhỏ hơn ngày trả phòng.";
                return RedirectToAction(nameof(Index));
            }

            var room = await _context.Rooms.Include(r => r.Hotel).Include(r => r.RoomType).FirstOrDefaultAsync(r => r.RoomID == RoomID);
            if (room == null || room.Status != "Available")
            {
                TempData["Error"] = "Phòng không khả dụng.";
                return RedirectToAction(nameof(Index));
            }

            var guest = await _context.Guests.FindAsync(GuestID);
            if (guest == null)
            {
                TempData["Error"] = "Khách hàng không tồn tại.";
                return RedirectToAction(nameof(Index));
            }

            double totalPrice = (CheckoutDate - CheckinDate).Days * 100.0;

            var booking = new Booking
            {
                GuestID = GuestID,
                RoomID = RoomID,
                CheckinDate = CheckinDate,
                CheckoutDate = CheckoutDate,
                TotalPrice = totalPrice,
                Room = room,
                Guest = guest
            };

            _context.Bookings.Add(booking);
            room.Status = "Booked";
            _context.Rooms.Update(room);

            await _context.SaveChangesAsync();

            TempData["Success"] = "Đặt phòng thành công.";
            return RedirectToAction(nameof(Index));
        }


        // POST: Manager/AddService
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddService(string ServiceName, string Description, double ServicePrice)
        {
            if (string.IsNullOrWhiteSpace(ServiceName) || string.IsNullOrWhiteSpace(Description) || ServicePrice <= 0)
            {
                TempData["Error"] = "Thông tin dịch vụ không hợp lệ.";
                return RedirectToAction(nameof(Index));
            }

            var service = new Service
            {
                Name = ServiceName,
                Description = Description,
                Price = ServicePrice
            };

            _context.Services.Add(service);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Dịch vụ đã được thêm thành công.";
            return RedirectToAction(nameof(Index));
        }

        // GET: Manager/EditBooking/{id}
        public async Task<IActionResult> EditBooking(int id)
        {
            var booking = await _context.Bookings
                .Include(b => b.Guest)
                .Include(b => b.Room)
                .FirstOrDefaultAsync(b => b.BookingID == id);

            if (booking == null)
            {
                return NotFound();
            }

            ViewBag.Guests = await _context.Guests.ToListAsync();
            ViewBag.Rooms = await _context.Rooms.ToListAsync();
            return View(booking);
        }

        // POST: Manager/EditBooking/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditBooking(int id, [Bind("BookingID,GuestID,RoomID,CheckinDate,CheckoutDate,TotalPrice")] Booking booking)
        {
            if (id != booking.BookingID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(booking);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Cập nhật thông tin đặt phòng thành công.";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Guests = await _context.Guests.ToListAsync();
            ViewBag.Rooms = await _context.Rooms.ToListAsync();
            return View(booking);
        }

        // GET: Manager/DeleteBooking/{id}
        public async Task<IActionResult> DeleteBooking(int id)
        {
            var booking = await _context.Bookings
                .Include(b => b.Room)
                .FirstOrDefaultAsync(b => b.BookingID == id);

            if (booking == null)
            {
                return NotFound();
            }

            _context.Bookings.Remove(booking);
            var room = booking.Room;
            if (room != null)
            {
                room.Status = "Available";
                _context.Rooms.Update(room);
            }

            await _context.SaveChangesAsync();
            TempData["Success"] = "Xóa đặt phòng thành công.";
            return RedirectToAction(nameof(Index));
        }
    }
}
