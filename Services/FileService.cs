using QuanLyKhachSan.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyKhachSan.Services
{
    public class FileService:IFileService
    {
        public string SaveFile(HttpPostedFileBase file)
        {
            string fileName = DateTime.Now.Ticks.ToString() + file.FileName;
            file.SaveAs(System.Web.HttpContext.Current.Server.MapPath("~/Content/images/" + fileName));
            return fileName;
        }
    }
}