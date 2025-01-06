using QuanLyKhachSan.Repositories;
using QuanLyKhachSan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyKhachSan.Controllers.Admin
{
    public class AdminHomeController : Controller
    {
        BookingRepository _bookingRepository;
        public AdminHomeController(BookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }
        // GET: AdminHome
        public ActionResult Index()
        {
            User user = (User)Session["ADMIN"];
            if (user == null)
            {
                return RedirectToAction("Login", "PublicAuthentication");
            }
            else
            {

                ViewBag.Month1 = _bookingRepository.GetTotalMoneyForMonthAsync(1);
                ViewBag.Month2 = _bookingRepository.GetTotalMoneyForMonthAsync(2);
                ViewBag.Month3 = _bookingRepository.GetTotalMoneyForMonthAsync(3);
                ViewBag.Month4 = _bookingRepository.GetTotalMoneyForMonthAsync(4);
                ViewBag.Month5 = _bookingRepository.GetTotalMoneyForMonthAsync(5);
                ViewBag.Month6 = _bookingRepository.GetTotalMoneyForMonthAsync(6);
                ViewBag.Month7 = _bookingRepository.GetTotalMoneyForMonthAsync(7);
                ViewBag.Month8 = _bookingRepository.GetTotalMoneyForMonthAsync(8);
                ViewBag.Month9 = _bookingRepository.GetTotalMoneyForMonthAsync(9);
                ViewBag.Month10 = _bookingRepository.GetTotalMoneyForMonthAsync(10);
                ViewBag.Month11 = _bookingRepository.GetTotalMoneyForMonthAsync(11);
                ViewBag.Month12 = _bookingRepository.GetTotalMoneyForMonthAsync(12);
                return View();
            }

        }
    }
}