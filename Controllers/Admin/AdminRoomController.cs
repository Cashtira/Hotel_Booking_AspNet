using QuanLyKhachSan.Repositories;
using QuanLyKhachSan.Models;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using QuanLyKhachSan.Services.Interface;
using System.Threading.Tasks;

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
        public async Task<ActionResult> Index(string msg)
        {
            ViewBag.Msg = msg;
            ViewBag.List = await _roomRepository.GetRoomsAsync();
            ViewBag.ListType = await _typeRepository.GetAllTypesAsync();
            return View();
        }

        // Thêm phòng
        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> Add(Room room)
        {
            var file = Request.Files["file"];
            if (file != null && file.ContentLength > 0)
            {
                string imageName = _fileService.SaveFile(file);
                room.image = imageName;
            }

            room.view = 0;
            await _roomRepository.AddRoomAsync(room);
            return RedirectToAction("Index", new { msg = "Room added successfully!" });
        }

        // Cập nhật phòng
        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> Update(Room room)
        {
            var existingRoom = _roomRepository.GetRoomByIdAsync(room.idRoom);
            if (existingRoom == null)
            {
                return RedirectToAction("Index", new { msg = "Room not found." });
            }

            var file = Request.Files["file"];
            string imageName = existingRoom.image;
            if (file != null && file.ContentLength > 0)
            {
                imageName = _fileService.SaveFile(file);
            }

            room.image = imageName;
            await _roomRepository.UpdateRoomAsync(room);
            return RedirectToAction("Index", new { msg = "Room updated successfully!" });
        }

        // Xóa phòng
        public async Task<ActionResult> Delete(int roomId)
        {
            var bookingExists = await _roomRepository.CheckRoomBookingsAsync(roomId);
            if (bookingExists.Count == 0)
            {
                await _roomRepository.DeleteRoomAsync(roomId);
                return RedirectToAction("Index", new { msg = "Room deleted successfully!" });
            }
            else
            {
                return RedirectToAction("Index", new { msg = "Cannot delete room, bookings exist." });
            }
        }
    }


}
