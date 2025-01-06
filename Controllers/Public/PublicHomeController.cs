using QuanLyKhachSan.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyKhachSan.Controllers.Public
{
    public class PublicHomeController : Controller
    {
        RoomRepository roomDao = new RoomRepository();
        ServiceRepository serviceDao = new ServiceRepository();
        TypeRepository typeDao = new TypeRepository();
        // GET: PublicHome
        public ActionResult Index()
        {
            ViewBag.ListRoomTop5 = roomDao.GetRoomTop5();
            ViewBag.ListRoomDiscount = roomDao.GetRoomDiscount();
            ViewBag.ListService = serviceDao.GetServicesTop5();
            ViewBag.ListType = typeDao.GetTypes();
            ViewBag.ListRoom = roomDao.GetRooms();  
            ViewBag.active = "home";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.active = "contact";
            return View();
        }

        public ActionResult AboutUs()
        {
            ViewBag.active = "aboutus";
            return View();
        }

        public ActionResult Introduce()
        {
            ViewBag.active = "introduce";
            return View();
        }
    }
}