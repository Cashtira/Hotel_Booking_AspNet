using Microsoft.AspNetCore.Mvc;

namespace MVCmodel.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
