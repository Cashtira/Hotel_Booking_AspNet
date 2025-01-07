using QuanLyKhachSan.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace QuanLyKhachSan.Controllers.Admin
{
    public class AdminServiceController : Controller
    {
        private readonly ServiceRepository _serviceRepository;
        public AdminServiceController(ServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }
        // GET: Adminservice
        public async Task<ActionResult> Index(string msg)
        {
            ViewBag.Msg = msg;
            ViewBag.List = await _serviceRepository.GetAllServicesAsync();
            return View();
        }
        public async Task<ActionResult> Add(QuanLyKhachSan.Models.Service service)
        {
            await _serviceRepository.AddServiceAsync(service);
            return RedirectToAction("Index", new { msg = "1" });
        }

        public async Task<ActionResult> Update(QuanLyKhachSan.Models.Service service)
        {
            await _serviceRepository.UpdateServiceAsync(service);
            return RedirectToAction("Index", new { msg = "1" });
        }

        public async Task<ActionResult> Delete(QuanLyKhachSan.Models.Service service)
        {
            var hasBookingExist = await _serviceRepository.HasBookingAsync(service.idService);
            if (!hasBookingExist)
            {
                await _serviceRepository.DeleteServiceAsync(service.idService);
                return RedirectToAction("Index", new { msg = "1" });
            }
            else
            {
                return RedirectToAction("Index", new { msg = "2" });
            }
        }
    }
}