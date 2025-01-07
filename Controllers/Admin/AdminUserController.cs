using QuanLyKhachSan.Repositories;
using QuanLyKhachSan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;

namespace QuanLyKhachSan.Controllers.Admin
{
    public class AdminUserController : Controller
    {
        private readonly UserRepository _userRepository;

        public AdminUserController(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }        // GET: AdminUser
        public async Task<ActionResult> Index(string msg)
        {
            ViewBag.Msg = msg;
            ViewBag.List = await _userRepository.GetEmployeesAsync();
            return View();
        }

        public async Task<ActionResult> Customer(string msg)
        {
            ViewBag.Msg = msg;
            ViewBag.List = await  _userRepository.GetCustomersAsync();
            return View();
        }
        public async Task<ActionResult> Add(User user)
        {
            user.idRole = 2;
            await _userRepository.AddUserAsync(user);
            return RedirectToAction("Index", new { msg = "1" });
        }

        public async Task<ActionResult> AddCustomer(User user)
        {
            user.idRole = 3;
            await _userRepository.AddUserAsync(user);
            return RedirectToAction("Customer", new { msg = "1" });
        }

        public async Task<ActionResult> Update(User user)
        {
            await _userRepository.UpdateUserAsync(user);
            return RedirectToAction("Index", new { msg = "1" });
        }

        public async Task<ActionResult> UpdateCustomer(User user)
        {
            await _userRepository.UpdateUserAsync(user);
            return RedirectToAction("Customer", new { msg = "1" });
        }

        public async Task<ActionResult> Delete(User user)
        {
            var hasBookings = await _userRepository.HasBookingsAsync(user.idUser);
            if (!hasBookings)
            {
                await _userRepository.DeleteUserAsync(user.idUser);
                return RedirectToAction("Index", new { msg = "1" });
            }
            else
            {
                return RedirectToAction("Index", new { msg = "2" });
            }
        }

        public async Task<ActionResult> DeleteCustomer(User user)
        {
            var hasBookings = await _userRepository.HasBookingsAsync(user.idUser);
            if (!hasBookings)
            {
                await _userRepository.DeleteUserAsync(user.idUser);
                return RedirectToAction("Customer", new { msg = "1" });
            }
            else
            {
                return RedirectToAction("Customer", new { msg = "2" });
            }
        }
    }
}