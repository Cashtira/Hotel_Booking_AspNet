using QuanLyKhachSan.Repositories;
using QuanLyKhachSan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;

namespace QuanLyKhachSan.Controllers.Admin
{
    public class AdminBookingController : Controller
    {
        private readonly BookingRepository _bookingRepository;
        public AdminBookingController(BookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        // GET: AdminUser
        public async Task<ActionResult> Index(string msg)
        {
            ViewBag.Msg = msg;
            ViewBag.List = await _bookingRepository.GetAllBookingAsync();
            return View();
        }

        public async Task<ActionResult> Detail(int id)
        {
            ViewBag.Booking = await _bookingRepository.GetBookingByIdAsync(id);
            ViewBag.List = await _bookingRepository.GetBookingServicesAsync(id);
            return View();
        }

        public async Task<ActionResult> Bill(int id)
        {
            ViewBag.Booking = await _bookingRepository.GetBookingByIdAsync(id);
            ViewBag.List = await _bookingRepository.GetBookingServicesAsync(id);
            return View();
        }

        public async Task<ActionResult> Update(Booking booking)
        {
            await _bookingRepository.UpdateBookingAsync(booking);
            return RedirectToAction("Index", new { msg = "1" });
        }


    }
}