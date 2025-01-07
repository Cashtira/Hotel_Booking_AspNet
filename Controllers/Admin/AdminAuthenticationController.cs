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
    public class AdminAuthenticationController : Controller
    {
        private readonly UserRepository _userRepository;
        public AdminAuthenticationController(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(FormCollection form)
        {
            User user = new User()
            {
                userName = form["userName"],
                password = form["password"]
            };
            string passwordMd5 = form["password"];
            bool checkLogin = await _userRepository.CheckLoginAsync(user.userName, passwordMd5);
            if (checkLogin)
            {
                var userInformation = await _userRepository.GetUserByUserNameAsync(user.userName);
                   
                if (userInformation.idRole == 3)
                {
                    ViewBag.mess = "Bạn không có quyền truy cập vào trang quản trị";
                    return View("Login");
                    }
                else
                {
                    Session.Add("ADMIN", userInformation);
                    return RedirectToAction("Index", "AdminHome");
                }

            }
            else
            {
                ViewBag.mess = "Thông tin tài khoản hoặc mật khẩu không chính xác";
                return View("Login");
            }
           
        }
        public ActionResult Logout()
        {
            Session.Remove("ADMIN");
            return Redirect("/AdminHome/Index");
        }
    }
}