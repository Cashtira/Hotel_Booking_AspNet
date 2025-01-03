using QuanLyKhachSan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyKhachSan.Daos
{
    public class RoomCommentDao
    {
        QuanLyKhachSanDBContext myDb = new QuanLyKhachSanDBContext();
        public void Add(RoomComment roomComment)
        {
            myDb.roomComments.Add(roomComment);
            myDb.SaveChanges();
        }

        public List<RoomComment> GetByIdRoom(int idRoom)
        {
            return myDb.roomComments.Where(x => x.idRoom == idRoom).OrderByDescending(x => x.createdDate).ToList();
        }

        public double getAve(int idRoom)
        {
            var qr =  myDb.roomComments.Where(x => x.idRoom == idRoom).ToList();
            return qr.Any() ? qr.Average(x => x.star) : 5;
        }
    }
}
