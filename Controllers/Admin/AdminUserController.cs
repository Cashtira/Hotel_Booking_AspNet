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
        UserRepository userDao = new UserRepository();
        // GET: AdminUser
        public ActionResult Index(string msg)
        {
            ViewBag.Msg = msg;
            ViewBag.List = userDao.getNV();
            return View();
        }

        public ActionResult Customer(string msg)
        {
            ViewBag.Msg = msg;
            ViewBag.List = userDao.getKH();
            return View();
        }
        public ActionResult Add(User user)
        {
            user.idRole = 2;
            user.password = userDao.md5(user.password);
            userDao.add(user);
            return RedirectToAction("Index", new { msg = "1" });
        }

        public ActionResult AddKH(User user)
        {
            user.idRole = 3;
            user.password = userDao.md5(user.password);
            userDao.add(user);
            return RedirectToAction("Customer", new { msg = "1" });
        }

        public ActionResult Update(User user)
        {
            userDao.update(user);
            return RedirectToAction("Index", new { msg = "1" });
        }

        public ActionResult UpdateKH(User user)
        {
            userDao.update(user);
            return RedirectToAction("Customer", new { msg = "1" });
        }

        public ActionResult Delete(User user)
        {
            var check = userDao.getCheck(user.idUser);
            if (check.Count == 0)
            {
                userDao.delete(user.idUser);
                return RedirectToAction("Index", new { msg = "1" });
            }
            else
            {
                return RedirectToAction("Index", new { msg = "2" });
            }
        }

        public ActionResult DeleteKH(User user)
        {
            var check = userDao.getCheck(user.idUser);
            if (check.Count == 0)
            {
                userDao.delete(user.idUser);
                return RedirectToAction("Customer", new { msg = "1" });
            }
            else
            {
                return RedirectToAction("Customer", new { msg = "2" });
            }
        }
    }
}