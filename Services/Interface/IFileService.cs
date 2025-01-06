using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace QuanLyKhachSan.Services.Interface
{
    public interface IFileService
    {
        string SaveFile(HttpPostedFileBase file);

    }
}
