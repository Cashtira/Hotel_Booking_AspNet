using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace QuanLyKhachSan
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
             name: "index page",
             url: "PublicRoom/Index/{page}",
             defaults: new { controller = "PublicRoom", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "PublicHome", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
    name: "RoomDetail",
    url: "PublicRoom/DetailRoom/{id}/{mess}",
    defaults: new { controller = "PublicRoom", action = "DetailRoom" }
);

        }
    }
}
