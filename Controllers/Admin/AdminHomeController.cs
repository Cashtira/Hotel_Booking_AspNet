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
        BookingRepository bookingDao = new BookingRepository();
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

                ViewBag.Month1 = bookingDao.GetTotalMoneyForMonthAsync(1);
                ViewBag.Month2 = bookingDao.GetTotalMoneyForMonthAsync(2);
                ViewBag.Month3 = bookingDao.GetTotalMoneyForMonthAsync(3);
                ViewBag.Month4 = bookingDao.GetTotalMoneyForMonthAsync(4);
                ViewBag.Month5 = bookingDao.GetTotalMoneyForMonthAsync(5);
                ViewBag.Month6 = bookingDao.GetTotalMoneyForMonthAsync(6);
                ViewBag.Month7 = bookingDao.GetTotalMoneyForMonthAsync(7);
                ViewBag.Month8 = bookingDao.GetTotalMoneyForMonthAsync(8);
                ViewBag.Month9 = bookingDao.GetTotalMoneyForMonthAsync(9);
                ViewBag.Month10 = bookingDao.GetTotalMoneyForMonthAsync(10);
                ViewBag.Month11 = bookingDao.GetTotalMoneyForMonthAsync(11);
                ViewBag.Month12 = bookingDao.GetTotalMoneyForMonthAsync(12);
                return View();
            }

        }
    }
}