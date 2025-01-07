using QuanLyKhachSan.Models;
using QuanLyKhachSan.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace QuanLyKhachSan.Controllers.Public
{
    public class PublicRoomController : Controller
    {
        private readonly RoomRepository _roomRepository;
        private readonly ServiceRepository _serviceRepository;
        private readonly BookingRepository _bookingRepository;
        private readonly BookingServiceRepository _bookingServiceRepository;
        private readonly RoomCommentRepository _roomCommentRepository;

        private readonly QuanLyKhachSanDBContext _context;
        public PublicRoomController(RoomRepository roomRepository, ServiceRepository serviceRepository, BookingRepository bookingRepository, BookingServiceRepository bookingServiceRepository, RoomCommentRepository roomCommentRepository, QuanLyKhachSanDBContext context)
        {
            _roomRepository = roomRepository;
            _serviceRepository = serviceRepository;
            _bookingRepository = bookingRepository;
            _bookingServiceRepository = bookingServiceRepository;
            _roomCommentRepository = roomCommentRepository;
            _context = context;
        }

        // GET: PublicRoom
        public async Task<ActionResult> Index(int page)
        {
            if (page == 0)
            {
                page = 1;
            }
            ViewBag.List = await _roomRepository.GetAvailableRoomsAsync(page, 3);
            ViewBag.tag = page;
            ViewBag.pageSize = await _roomRepository.GetTotalAvailableRoomGroupsAsync();
            return View();
        }

        public async Task<ActionResult> DetailRoom(int id, string mess)
        {
            ViewBag.mess = mess;

            // Chờ tất cả các phương thức bất đồng bộ
            ViewBag.listComment = await _roomCommentRepository.GetRoomCommentsByRoomIdAsync(id);
            ViewBag.Ave = await _roomCommentRepository.GetAverageStarByRoomIdAsync(id); // Đảm bảo gọi bất đồng bộ này với await
            await _roomRepository.UpdateRoomViewAsync(id);

            // Lấy chi tiết phòng
            Room room = await _roomRepository.GetRoomDetailsAsync(id);
            ViewBag.Room = room;

            // Lấy danh sách dịch vụ và phòng liên quan
            ViewBag.ListService = await _serviceRepository.GetAllServicesAsync(); // Đảm bảo gọi bất đồng bộ này với await
            ViewBag.ListRoomRelated = await _roomRepository.GetRoomsByTypeAsync(room.idType); // Đảm bảo gọi bất đồng bộ này với await

            return View();
        }

        /*[HttpPost]
        public ActionResult Booking(Booking booking,int[] idService)
        {
            User user = (User)Session["USER"];
            string action = "DetailRoom/" + booking.idRoom;
            if (user == null)
            {              
                return RedirectToAction(action, new { mess = "ErrorLogin" });
            }
            else
            {
                Booking checkExist = _bookingRepository.CheckBooking(booking.idRoom);
                int priceService = 0;
                if (idService != null)
                {                 
                    for (int i = 0; i < idService.Count(); i++)
                    {

                        priceService += _serviceRepository.GetCostById(idService[i]);
                    }
                }
                
                if (checkExist == null || (checkExist != null && DateTime.Now > DateTime.Parse(checkExist.checkOutDate)))
                {
                    DateTime dateCheckout = DateTime.Parse(booking.checkOutDate);
                    DateTime dateCheckin = DateTime.Parse(booking.checkInDate);
                    int numberBooking = dateCheckout.Day - dateCheckin.Day;
                    Room room = _roomRepository.GetDetail(booking.idRoom);
                    booking.idUser = user.idUser;
                    booking.createdDate = DateTime.Now;
                    booking.isPayment = false;
                    booking.status = 0;
                    booking.totalMoney = (room.cost * numberBooking - room.cost * numberBooking * room.discount / 100) + priceService;
                    _bookingRepository.Add(booking);
                    if(idService != null)
                    {
                       for(int i = 0; i < idService.Count(); i++)
                       {
                            BookingService obj = new BookingService
                            {
                                idService = idService[i],
                                idBooking = booking.idBooking
                            };
                            bookingServiceDao.Add(obj);
                       }
                    }
                    return RedirectToAction(action, new { mess = "Success" });
                }
                else
                {
                    return RedirectToAction(action, new { mess = "ErrorExist" });
                }
            }
        }*/


        [HttpPost]
        public async Task<ActionResult> Booking(Booking booking, int[] idService)
        {
            User user = (User)Session["USER"];
            string action = $"DetailRoom/{booking.idRoom}";

            if (user == null)
            {
                return RedirectToAction(action, new { mess = "ErrorLogin" });
            }

            var existingBookings =await  _bookingRepository.GetBookedRoomAsync(booking.idRoom);
            int priceService = 0;
            if (idService != null)
            {
                foreach (var serviceId in idService)
                {
                    var room = await _roomRepository.GetRoomDetailsAsync(serviceId);
                    if (room != null)
                    {
                        priceService += room.cost; // Giả sử phòng có thuộc tính Cost
                    }
                }
            }

            if (existingBookings.Count()==0)
            {
                if (!IsValidBookingDates(booking.checkInDate, booking.checkOutDate))
                {
                    return RedirectToAction(action, new { mess = "Error" });
                }

                int numberBooking = GetBookingDays(booking.checkInDate, booking.checkOutDate);
                Room room = await _roomRepository.GetRoomDetailsAsync(booking.idRoom);

                booking.idUser = user.idUser;
                booking.createdDate = DateTime.Now;
                booking.isPayment = false;
                booking.status = 0;
                booking.totalMoney = CalculateTotalPrice(room, numberBooking, priceService);

                await _bookingRepository.AddBookingAsync(booking);
                await AddServicesToBookingAsync(idService, booking.idBooking);

                return RedirectToAction(action, new { mess = "Success" });
            }
            else
            {
                if (HasBookingConflict(existingBookings, booking.checkInDate, booking.checkOutDate))
                {
                    return RedirectToAction(action, new { mess = "ErrorExist" });
                }

                if (!IsValidBookingDates(booking.checkInDate, booking.checkOutDate))
                {
                    return RedirectToAction(action, new { mess = "Error" });
                }

                int numberBooking = GetBookingDays(booking.checkInDate, booking.checkOutDate);
                Room room = await _roomRepository.GetRoomDetailsAsync(booking.idRoom);

                booking.idUser = user.idUser;
                booking.createdDate = DateTime.Now;
                booking.isPayment = false;
                booking.status = 0;
                booking.totalMoney = CalculateTotalPrice(room, numberBooking, priceService);

                await _bookingRepository.AddBookingAsync(booking);
                await AddServicesToBookingAsync(idService, booking.idBooking);

                return RedirectToAction(action, new { mess = "Success" });
            }
        }

        // Các hàm helper được để ở dưới controller
        private bool IsValidBookingDates(string checkInDate, string checkOutDate)
        {
            DateTime dateCheckin = DateTime.Parse(checkInDate);
            DateTime dateCheckout = DateTime.Parse(checkOutDate);
            return dateCheckout > dateCheckin;
        }

        private int GetBookingDays(string checkInDate, string checkOutDate)
        {
            DateTime dateCheckin = DateTime.Parse(checkInDate);
            DateTime dateCheckout = DateTime.Parse(checkOutDate);
            TimeSpan time = dateCheckout - dateCheckin;
            return time.Days;
        }

        private int CalculateTotalPrice(Room room, int numberBooking, int priceService)
        {
            return (room.cost * numberBooking - room.cost * numberBooking * room.discount / 100) + priceService;
        }

        private bool HasBookingConflict(List<Booking> existingBookings, string checkInDate, string checkOutDate)
        {
            DateTime dateCheckin = DateTime.Parse(checkInDate);
            DateTime dateCheckout = DateTime.Parse(checkOutDate);
            foreach (var existingBooking in existingBookings)
            {
                DateTime existingCheckin = DateTime.Parse(existingBooking.checkInDate);
                DateTime existingCheckout = DateTime.Parse(existingBooking.checkOutDate);

                if ((dateCheckin <= existingCheckout && dateCheckin >= existingCheckin) ||
                    (dateCheckout <= existingCheckout && dateCheckout >= existingCheckin))
                {
                    return true;
                }
            }
            return false;
        }

        private async Task AddServicesToBookingAsync(int[] idService, int idBooking)
        {
            if (idService != null)
            {
                foreach (var serviceId in idService)
                {
                    BookingService obj = new BookingService
                    {
                        idService = serviceId,
                        idBooking = idBooking
                    };
                    await _bookingServiceRepository.AddBookingServiceAsync(obj);
                }
            }
        }

        [HttpPost]
        public ActionResult Search(FormCollection form)
        {
            string name = form["name"];
            int idType = Int32.Parse(form["idType"]);
            int numberChildren = Int32.Parse(form["numberChildren"]);
            int numberAdult = Int32.Parse(form["numberAdult"]);
            return RedirectToAction("Search", new { page = 0, name = name, idType = idType, numberChildren = numberChildren,numberAdult = numberAdult });
        }

        [HttpGet]
        public async Task<ActionResult> Search(int page,string name , int idType,int numberChildren, int numberAdult)
        {
            if (page == 0)
            {
                page = 1;
            }
            if (name == null && idType != 0)
            {
                ViewBag.List = await _roomRepository.SearchRoomsByTypeAsync(page, 3, idType,numberChildren,numberAdult);
                ViewBag.tag = page;
                ViewBag.key = 1;
                ViewBag.idType = idType;
                ViewBag.numberChildren = numberChildren;
                ViewBag.numberAdult = numberAdult;
                ViewBag.pageSize = await _roomRepository.GetTotalAvailableRoomGroupsByTypeAsync(idType, numberChildren, numberAdult);
            }
            else if(name != null && idType == 0)
            {
                ViewBag.List = await _roomRepository.SearchRoomsByNameAsync(page, 3, name, numberChildren, numberAdult);
                ViewBag.tag = page;
                ViewBag.key = 2;
                ViewBag.name = name;
                ViewBag.numberChildren = numberChildren;
                ViewBag.numberAdult = numberAdult;
                ViewBag.pageSize = await _roomRepository.GetTotalAvailableRoomGroupsByNameAsync(name, numberChildren, numberAdult);
            } else if (name != null && idType != 0)
            {
                ViewBag.List = await _roomRepository.SearchRoomsByTypeAndNameAsync(page, 3,idType, name, numberChildren, numberAdult);
                ViewBag.tag = page;
                ViewBag.key = 3;
                ViewBag.name = name;
                ViewBag.idType = idType;
                ViewBag.numberChildren = numberChildren;
                ViewBag.numberAdult = numberAdult;
                ViewBag.pageSize =await _roomRepository.GetTotalAvailableRoomGroupsByNameAndTypeAsync(name,idType, numberChildren, numberAdult);
            }
            else if (name == null && idType == 0)
            {
                List<Room> list = new List<Room>();
                ViewBag.List = list;
                RedirectToAction("Search", "PublicRoom");
            }
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> PostComment(string comment, int idRoom, int star)
        {
            User user = (User)Session["USER"];
            await _roomCommentRepository.AddRoomCommentAsync(new RoomComment
            {
                createdDate = DateTime.Now,
                idRoom = idRoom,
                text = comment,
                idUser = user.idUser,
                star = star
            });
            return Json(true, JsonRequestBehavior.AllowGet);
        }

    }
}