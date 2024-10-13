using Microsoft.AspNetCore.Mvc;

namespace MVCmodel.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
