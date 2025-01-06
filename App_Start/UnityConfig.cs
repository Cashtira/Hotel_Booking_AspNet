using QuanLyKhachSan.Models;
using QuanLyKhachSan.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Unity;
using Unity.AspNet.Mvc;

namespace QuanLyKhachSan.App_Start
{
    public class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            container.RegisterType<QuanLyKhachSanDBContext>(); 
            container.RegisterType<BookingRepository>();
            container.RegisterType<BookingServiceRepository>();
            container.RegisterType<RoomCommentRepository>();
            container.RegisterType<RoomRepository>();
            container.RegisterType<ServiceRepository>();
            container.RegisterType<TypeRepository>();
            container.RegisterType<UserRepository>();

           DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}