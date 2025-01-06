using QuanLyKhachSan.Repositories;
using QuanLyKhachSan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyKhachSan.Controllers.Admin
{
    public class AdminBookingController : Controller
    {
        BookingRepository _context = new BookingRepository();
        // GET: AdminUser
        public ActionResult Index(string msg)
        {
            ViewBag.Msg = msg;
            ViewBag.List = _context.();
            return View();
        }

        public ActionResult Detail(int id)
        {
            ViewBag.Booking = _context.GetBookingByIdAsync(id);
            ViewBag.List = _context.GetBookingServicesAsync(id);
            return View();
        }

        public ActionResult Bill(int id)
        {
            ViewBag.Booking = _context.GetBookingByIdAsync(id);
            ViewBag.List = _context.GetBookingServicesAsync(id);
            return View();
        }

        public ActionResult Update(Booking booking)
        {
            _context.UpdateBookingAsync(booking);
            return RedirectToAction("Index", new { msg = "1" });
        }


    }
}