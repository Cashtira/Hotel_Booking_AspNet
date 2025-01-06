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
        TypeRepository _typeRepository;
        public AdminTypeController(TypeRepository typeRepository)
        {
            _typeRepository = typeRepository;
        }

        // GET: AdminType
        public async Task<ActionResult> IndexAsync(string msg)
        {
            ViewBag.Msg = msg;
            ViewBag.List = await _typeRepository.GetTypesAsync();
            return View();
        }
        public async Task<ActionResult> AddAsync(QuanLyKhachSan.Models.Type type)
        {
            await _typeRepository.AddTypeAsync(type);
            return RedirectToAction("Index", new { msg = "1" });
        }

        public async Task<ActionResult> UpdateAsync(QuanLyKhachSan.Models.Type type)
        {
           await _typeRepository.UpdateTypeAsync(type);
            return RedirectToAction("Index", new { msg = "1" });
        }

        public ActionResult Delete(QuanLyKhachSan.Models.Type type)
        {
            var check = _typeRepository.getRoomType(type.idType);
            if(check.Count == 0)
            {
                _typeRepository.delete(type.idType);
                return RedirectToAction("Index", new { msg = "1" });
            }
            else
            {
                return RedirectToAction("Index", new { msg = "2" });
            }
        }
    }
}