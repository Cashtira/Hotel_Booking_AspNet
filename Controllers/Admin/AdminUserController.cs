using QuanLyKhachSan.Repositories;
using QuanLyKhachSan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyKhachSan.Controllers.Admin
{
    public class AdminUserController : Controller
    {
        private readonly UserRepository _userRepository;

        public AdminUserController()
        {
            var context = new QuanLyKhachSanDBContext();
            _userRepository = new UserRepository(context);
        }        // GET: AdminUser
        public ActionResult Index(string msg)
        {
            ViewBag.Msg = msg;
            ViewBag.List = _userRepository.GetEmployees();
            return View();
        }

        public ActionResult Customer(string msg)
        {
            ViewBag.Msg = msg;
            ViewBag.List = _userRepository.GetCustomers();
            return View();
        }
        public ActionResult Add(User user)
        {
            user.idRole = 2;
            user.password = _userRepository.Md5Hash(user.password);
            _userRepository.AddUser(user);
            return RedirectToAction("Index", new { msg = "1" });
        }

        public ActionResult AddKH(User user)
        {
            user.idRole = 3;
            user.password = _userRepository.Md5Hash(user.password);
            _userRepository.AddUser(user);
            return RedirectToAction("Customer", new { msg = "1" });
        }

        public ActionResult Update(User user)
        {
            _userRepository.UpdateUser(user);
            return RedirectToAction("Index", new { msg = "1" });
        }

        public ActionResult UpdateKH(User user)
        {
            _userRepository.UpdateUser(user);
            return RedirectToAction("Customer", new { msg = "1" });
        }

        public ActionResult Delete(User user)
        {
            var check = _userRepository.getCheck(user.idUser);
            if (check.Count == 0)
            {
                _userRepository.DeleteUser(user.idUser);
                return RedirectToAction("Index", new { msg = "1" });
            }
            else
            {
                return RedirectToAction("Index", new { msg = "2" });
            }
        }

        public ActionResult DeleteKH(User user)
        {
            var check = _userRepository.getCheck(user.idUser);
            if (check.Count == 0)
            {
                _userRepository.delete(user.idUser);
                return RedirectToAction("Customer", new { msg = "1" });
            }
            else
            {
                return RedirectToAction("Customer", new { msg = "2" });
            }
        }
    }
}