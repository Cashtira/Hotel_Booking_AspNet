using QuanLyKhachSan.Repositories;
using QuanLyKhachSan.Models;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using QuanLyKhachSan.Services.Interface;

namespace QuanLyKhachSan.Controllers.Admin
{
    public class AdminRoomController : Controller
    {
        private readonly RoomRepository _roomRepository;
        private readonly TypeRepository _typeRepository;
        private readonly IFileService _fileService;

        // Constructor sử dụng Dependency Injection
        public AdminRoomController(
            RoomRepository roomRepository,
            TypeRepository typeRepository,
            IFileService fileService)
        {
            _roomRepository = roomRepository;
            _typeRepository = typeRepository;
            _fileService = fileService;
        }

        // GET: AdminRoom
        public ActionResult Index(string msg)
        {
            ViewBag.Msg = msg;
            ViewBag.List = roomDao.GetRooms();
            ViewBag.ListType = _typeRepository.GetTypes();
            return View();
        }

        // Thêm phòng
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(Room room)
        {
            var file = Request.Files["file"];
            if (file != null && file.ContentLength > 0)
            {
                string imageName = fileService.SaveFile(file);
                room.image = imageName;
            }

            room.view = 0;
            roomDao.AddRoom(room);
            return RedirectToAction("Index", new { msg = "Room added successfully!" });
        }

        // Cập nhật phòng
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Update(Room room)
        {
            var existingRoom = roomDao.GetDetail(room.idRoom);
            if (existingRoom == null)
            {
                return RedirectToAction("Index", new { msg = "Room not found." });
            }

            var file = Request.Files["file"];
            string imageName = existingRoom.image; // Dùng hình ảnh cũ nếu không upload ảnh mới
            if (file != null && file.ContentLength > 0)
            {
                imageName = fileService.SaveFile(file);
            }

            room.image = imageName;
            roomDao.UpdateRoom(room);
            return RedirectToAction("Index", new { msg = "Room updated successfully!" });
        }

        // Xóa phòng
        public ActionResult Delete(int roomId)
        {
            var bookingExists = roomDao.CheckRoomBookings(roomId);
            if (bookingExists.Count == 0)
            {
                roomDao.DeleteRoom(roomId);
                return RedirectToAction("Index", new { msg = "Room deleted successfully!" });
            }
            else
            {
                return RedirectToAction("Index", new { msg = "Cannot delete room, bookings exist." });
            }
        }
    }


}
