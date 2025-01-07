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
            else
            {

                ViewBag.Month1 = await _bookingRepository.GetTotalMoneyForMonthAsync(1);
                ViewBag.Month2 = await _bookingRepository.GetTotalMoneyForMonthAsync(2);
                ViewBag.Month3 = await _bookingRepository.GetTotalMoneyForMonthAsync(3);
                ViewBag.Month4 = await _bookingRepository.GetTotalMoneyForMonthAsync(4);
                ViewBag.Month5 = await _bookingRepository.GetTotalMoneyForMonthAsync(5);
                ViewBag.Month6 = await _bookingRepository.GetTotalMoneyForMonthAsync(6);
                ViewBag.Month7 = await _bookingRepository.GetTotalMoneyForMonthAsync(7);
                ViewBag.Month8 = await _bookingRepository.GetTotalMoneyForMonthAsync(8);
                ViewBag.Month9 = await _bookingRepository.GetTotalMoneyForMonthAsync(9);
                ViewBag.Month10 = await _bookingRepository.GetTotalMoneyForMonthAsync(10);
                ViewBag.Month11 = await _bookingRepository.GetTotalMoneyForMonthAsync(11);
                ViewBag.Month12 = await _bookingRepository.GetTotalMoneyForMonthAsync(12);
                return View();
            }

        }
    }
}