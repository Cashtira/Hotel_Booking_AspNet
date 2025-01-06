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
        RoomRepository _roomRepository;
        ServiceRepository _serviceRepository;
        TypeRepository _typeRepository;
        public PublicHomeController(RoomRepository roomRepository, ServiceRepository serviceRepository, TypeRepository typeRepository)
        {
            _roomRepository = roomRepository;
            _serviceRepository = serviceRepository;
            _typeRepository = typeRepository;
        }
        // GET: PublicHome
        public ActionResult Index()
        {
            ViewBag.ListRoomTop5 = _roomRepository.GetRoomTop5();
            ViewBag.ListRoomDiscount = _roomRepository.GetRoomDiscount();
            ViewBag.ListService = _serviceRepository.GetServicesTop5();
            ViewBag.ListType = _typeRepository.GetTypes();
            ViewBag.ListRoom = _roomRepository.GetRoomsAsync();  
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