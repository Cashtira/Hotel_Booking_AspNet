using QuanLyKhachSan.Repositories;
using QuanLyKhachSan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyKhachSan.Controllers.Admin
{
    public class AdminAuthenticationController : Controller
    {
        private readonly UserRepository _userRepository;

        public AdminAuthenticationController()
        {
            var context = new QuanLyKhachSanDBContext();  
            _userRepository = new UserRepository(context);  
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
        public ActionResult Login(FormCollection form)
        {
            User user = new User()
            {
                userName = form["userName"],
                password = form["password"]
            };
            string passwordMd5 = _userRepository.Md5Hash(form["password"]);
            bool checkLogin = _userRepository.CheckLogin(user.userName, passwordMd5);
            if (checkLogin)
            {
                var userInformation = _userRepository.GetUserByUserName(user.userName);
                   
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