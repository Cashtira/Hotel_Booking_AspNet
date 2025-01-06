using QuanLyKhachSan.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyKhachSan.Controllers.Admin
{
    public class AdminTypeController : Controller
    {
        TypeRepository _typeRepository = new TypeRepository();
        // GET: AdminType
        public ActionResult Index(string msg)
        {
            ViewBag.Msg = msg;
            ViewBag.List = _typeRepository.GetTypes();
            return View();
        }
        public ActionResult Add(QuanLyKhachSan.Models.Type type)
        {
            _typeRepository.add(type);
            return RedirectToAction("Index", new { msg = "1" });
        }

        public ActionResult Update(QuanLyKhachSan.Models.Type type)
        {
            _typeRepository.update(type);
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