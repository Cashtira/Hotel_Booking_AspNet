using QuanLyKhachSan.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace QuanLyKhachSan.Controllers.Admin
{
    public class AdminTypeController : Controller
    {
        private readonly TypeRepository _typeRepository;
        public AdminTypeController(TypeRepository typeRepository)
        {
            _typeRepository = typeRepository;
        }

        // GET: AdminType
        public async Task<ActionResult> Index(string msg)
        {
            ViewBag.Msg = msg;
            ViewBag.List = await _typeRepository.GetAllTypesAsync();
            return View();
        }
        public async Task<ActionResult> Add(QuanLyKhachSan.Models.Type type)
        {
            await _typeRepository.AddTypeAsync(type);
            return RedirectToAction("Index", new { msg = "1" });
        }

        public async Task<ActionResult> Update(QuanLyKhachSan.Models.Type type)
        {
           await _typeRepository.UpdateTypeAsync(type);
            return RedirectToAction("Index", new { msg = "1" });
        }

        public async Task<ActionResult> Delete(QuanLyKhachSan.Models.Type type)
        {
            var rooms = await _typeRepository.GetRoomsByTypeAsync(type.idType);

            if (rooms.Count == 0)
            {
                await _typeRepository.DeleteTypeAsync(type.idType);
                return RedirectToAction("Index", new { msg = "1" });
            }
            else
            {
                return RedirectToAction("Index", new { msg = "2" });
            }
        }

    }
}