using QuanLyKhachSan.Repositories;
using QuanLyKhachSan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;

namespace QuanLyKhachSan.Controllers.Public
{
    public class PublicUserController : Controller
    {
        UserRepository _userRepository;
        private readonly QuanLyKhachSanDBContext _context;
        public PublicUserController(UserRepository userRepository, QuanLyKhachSanDBContext context)
        {
            _userRepository = userRepository;
            _context = context;
        }
        // GET: PublicUser
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> ProfileUser(int id,string mess)
        {
            ViewBag.profile = await _userRepository.GetUserInfoAsync(id);
            ViewBag.mess = mess;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> UpdateProfile(User user)
        {
            await _userRepository.UpdateUserAsync(user);
            return RedirectToAction("ProfileUser", new { id = user.idUser, mess = "Success" });
        }

        [HttpPost]
        public ActionResult UpdatePassword(FormCollection form)
        {
            string passwordNew = form["password"];
            string rePasswordNew = form["rePassword"];
            int id = Int32.Parse(form["idUser"]);
            if (passwordNew.Equals(rePasswordNew))
            {
                User user = _context.Users.FirstOrDefault(x => x.idUser == id);
                user.password = passwordNew;
                _context.SaveChanges();
                return RedirectToAction("ProfileUser", new { id = id, mess = "Success" });
            }
            else
            {
                return RedirectToAction("ProfileUser", new { id = id, mess = "Error" });
            }
                    
        }
    }
}