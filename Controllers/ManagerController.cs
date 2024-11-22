using Microsoft.AspNetCore.Mvc;

namespace MVCmodel.Controllers
{
    public class ManagerController : Controller
    {
      
        public IActionResult Index()
        {
            return View();
        }
    }
}
