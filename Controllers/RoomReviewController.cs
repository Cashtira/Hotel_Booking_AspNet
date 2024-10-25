using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVCmodel.Models;
using System.Diagnostics;

namespace MVCmodel.Controllers
{
    public class RoomReviewController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public RoomReviewController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
