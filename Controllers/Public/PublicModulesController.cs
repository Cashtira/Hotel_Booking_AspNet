using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyKhachSan.Controllers.Public
{
    public class PublicModulesController : Controller
    {
        // GET: PublicModules
       // [ChildActionOnly]
        public ActionResult Header()
        {
            return PartialView();
        }

       // [ChildActionOnly]
        public ActionResult Footer()
        {
            return PartialView();
        }    
    }
}