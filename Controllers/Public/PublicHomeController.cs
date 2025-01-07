using QuanLyKhachSan.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace QuanLyKhachSan.Controllers.Public
{
    public class PublicHomeController : Controller
    {
        private readonly RoomRepository _roomRepository;
        private readonly ServiceRepository _serviceRepository;
        private readonly TypeRepository _typeRepository;
        public PublicHomeController(RoomRepository roomRepository, ServiceRepository serviceRepository, TypeRepository typeRepository)
        {
            _roomRepository = roomRepository;
            _serviceRepository = serviceRepository;
            _typeRepository = typeRepository;
        }
        // GET: PublicHome
        public async Task<ActionResult> Index()
        {
            ViewBag.ListRoomTop5 = await  _roomRepository.GetTopRoomsByViewAsync();
            ViewBag.ListRoomDiscount =await  _roomRepository.GetDiscountedRoomsAsync();
            ViewBag.ListService =await  _serviceRepository.GetTopServicesAsync();
            ViewBag.ListType = await _typeRepository.GetAllTypesAsync();
            ViewBag.ListRoom = await _roomRepository.GetRoomsAsync();  
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