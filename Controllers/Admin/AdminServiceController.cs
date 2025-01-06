using QuanLyKhachSan.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace QuanLyKhachSan.Controllers.Admin
{
    public class AdminServiceController : Controller
    {
        ServiceRepository _serviceRepository = new ServiceRepository();
        // GET: Adminservice
        public ActionResult Index(string msg)
        {
            ViewBag.Msg = msg;
            ViewBag.List = _serviceRepository.GetAllServices();
            return View();
        }
        public ActionResult Add(QuanLyKhachSan.Models.Service service)
        {
            _serviceRepository.add(service);
            return RedirectToAction("Index", new { msg = "1" });
        }

        public ActionResult Update(QuanLyKhachSan.Models.Service service)
        {
            _serviceRepository.update(service);
            return RedirectToAction("Index", new { msg = "1" });
        }

        public ActionResult Delete(QuanLyKhachSan.Models.Service service)
        {
            var check = _serviceRepository.getCheck(service.idService);
            if (check.Count == 0)
            {
                _serviceRepository.delete(service.idService);
                return RedirectToAction("Index", new { msg = "1" });
            }
            else
            {
                return RedirectToAction("Index", new { msg = "2" });
            }
        }
    }
}