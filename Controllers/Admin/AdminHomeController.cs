using QuanLyKhachSan.Repositories;
using QuanLyKhachSan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Data.Entity;
using QuanLyKhachSan.ViewModel;

namespace QuanLyKhachSan.Controllers.Admin
{
    public class AdminHomeController : Controller
    {
        private readonly BookingRepository _bookingRepository;
        private readonly QuanLyKhachSanDBContext _context;

        public AdminHomeController(BookingRepository bookingRepository, QuanLyKhachSanDBContext context)
        {
            _bookingRepository = bookingRepository;
            _context = context;
        }

        // GET: AdminHome
        public async Task<ActionResult> Index()
        {
            User user = (User)Session["ADMIN"];
            if (user == null)
            {
                return RedirectToAction("Login", "PublicAuthentication");
            }

            // Fetch statistics data

            var statistics = await GetStatisticsAsync();

            // Passing statistics to the view
            ViewBag.UserCount = statistics.UserCount;
            ViewBag.BookingCount = statistics.BookingCount;
            ViewBag.BookingCompletedCount = statistics.BookingCompletedCount;
            ViewBag.BookingCancelledCount = statistics.BookingCancelledCount;
            ViewBag.BookingsToday = statistics.BookingsToday;
            ViewBag.UpcomingBookings = statistics.UpcomingBookings;
            ViewBag.TotalRevenue = statistics.TotalRevenue;
            ViewBag.AverageRevenuePerBooking = statistics.AverageRevenuePerBooking;
            ViewBag.MonthlyData = statistics.MonthlyData;

            return View(statistics);
        }

        private async Task<StatisticViewModel> GetStatisticsAsync()
        {
            // Statistics data
            var userCount = _context.Users.Count();
            var bookingCount = _context.Bookings.Count();
            var bookingCompletedCount = _context.Bookings.Count(b => b.status == 1);
            var bookingCancelledCount = _context.Bookings.Count(b => b.status == 2);

            // Fetch all bookings and filter them in memory
            var bookings = _context.Bookings.ToList();

            // Filter bookings for today
            var bookingsToday = bookings
                .Where(b => DateTime.TryParse(b.checkInDate, out DateTime checkIn) && checkIn.Date == DateTime.Now.Date)
                .Count();

            // Filter upcoming bookings
            var upcomingBookings = bookings.Count(b => DateTime.TryParse(b.checkInDate, out DateTime checkIn) && checkIn > DateTime.Now);

            var totalRevenue = await _bookingRepository.GetTotalRevenueAsync();
            var averageRevenuePerBooking = bookingCount > 0 ? totalRevenue / bookingCount : 0;

            // Monthly data for 12 months
            var monthlyData = new List<int>();
            for (int month = 1; month <= 12; month++)
            {
                monthlyData.Add(await _bookingRepository.GetTotalMoneyForMonthAsync(month));
            }

            return new StatisticViewModel
            {
                UserCount = userCount,
                BookingCount = bookingCount,
                BookingCompletedCount = bookingCompletedCount,
                BookingCancelledCount = bookingCancelledCount,
                BookingsToday = bookingsToday,
                UpcomingBookings = upcomingBookings,
                TotalRevenue = totalRevenue,
                AverageRevenuePerBooking = averageRevenuePerBooking,
                MonthlyData = monthlyData
            };
        }
    }

  
}
