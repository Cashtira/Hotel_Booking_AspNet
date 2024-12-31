using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVCmodel.Models;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MVCmodel.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUserModel> _userManager;
        private readonly SignInManager<AppUserModel> _signInManager;

        public AccountController(SignInManager<AppUserModel> signInManager, UserManager<AppUserModel> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserModel user)
        {
            if (string.IsNullOrEmpty(user.UserName) || string.IsNullOrEmpty(user.Password))
            {
                ViewData["ErrorMessage"] = "Login Failed";
                return View(user);
            }
            // Tìm người dùng bằng username
            var appUser = await _userManager.FindByNameAsync(user.UserName);

                if (appUser != null)
                {
                    // Kiểm tra mật khẩu
                    var result = await _signInManager.PasswordSignInAsync(appUser, user.Password, false, false);

                    if (result.Succeeded)
                    {
                        if (user.UserName == "admin")
                        {
                        // Nếu username là "admin", chuyển hướng đến trang Bookings
  
                            return RedirectToAction("Index1", "Admin");
                        }
                        else
                        {
                             // Nếu username khác, chuyển hướng đến trang Guests
                             return RedirectToAction("Index", "Manager");
                        }
                    }
                    else
                    {
                        ViewData["ErrorMessage"] = "Wrong password.";
                        return View(user);
                    }
                }
                else
                {
                    ViewData["ErrorMessage"] = "No user found with this username.";
                    return View(user);
                }
          
        }

        // Phương thức GET để hiển thị form đăng ký
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        public async Task<bool> IsUserNameExist(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            return user != null; // Trả về true nếu người dùng đã tồn tại
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserModel user)
        {

            if (ModelState.IsValid)
            {
                if (await IsUserNameExist(user.UserName))
                {
                    ModelState.AddModelError("UserName", "The username is already taken.");
                    return View(user);
                }
                // Tạo người dùng mới mà không kiểm tra mật khẩu ngay
                AppUserModel newUser = new AppUserModel
                {
                    UserName = user.UserName,
                    Email = user.Email,
                };
                // Kiểm tra tính hợp lệ của mật khẩu sau khi tạo người dùng
                var passwordValidationResult = await _userManager.PasswordValidators[0].ValidateAsync(_userManager, newUser, user.Password);

                if (!passwordValidationResult.Succeeded)
                {
                    // Nếu mật khẩu không hợp lệ, xóa người dùng đã tạo và trả về lỗi
                    await _userManager.DeleteAsync(newUser);

                    // Thêm lỗi vào ModelState
                    foreach (var error in passwordValidationResult.Errors)
                    {
                        ModelState.AddModelError("Password", error.Description);
                    }

                    return View(user); // Trả về form với lỗi mật khẩu không hợp lệ
                }

                // Tạo người dùng mà không có mật khẩu (mật khẩu chưa được kiểm tra)
                IdentityResult result = await _userManager.CreateAsync(newUser, user.Password);

                // Kiểm tra kết quả tạo người dùng
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(user); // Trả về form nếu không tạo được người dùng
                }

            
                // Nếu mật khẩu hợp lệ, lưu mật khẩu vào cơ sở dữ liệu
                TempData["Success"] = "User created successfully";
                return Redirect("/Account/Index");
            }

            return View(user); // Trả về form nếu Model không hợp lệ
        }

    }
}
