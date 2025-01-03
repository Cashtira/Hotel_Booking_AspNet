using QuanLyKhachSan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyKhachSan.Daos
{
    public class TypeDao
    {
        QuanLyKhachSanDBContext myDb = new QuanLyKhachSanDBContext();

        public List<QuanLyKhachSan.Models.Type> GetTypes()
        {
            return myDb.types.ToList();
        }

        public void add(QuanLyKhachSan.Models.Type type)
        {
            myDb.types.Add(type);
            myDb.SaveChanges();
        }

        public void delete(int id)
        {
              var obj = myDb.types.FirstOrDefault(x => x.idType == id);
              myDb.types.Remove(obj);
              myDb.SaveChanges();
        }

        public void update(QuanLyKhachSan.Models.Type type)
        {
            var obj = myDb.types.FirstOrDefault(x => x.idType == type.idType);
            obj.name = type.name;
            myDb.SaveChanges();
        }

        public QuanLyKhachSan.Models.Type getTypeId(int id)
        {
            return myDb.types.FirstOrDefault(x => x.idType == id);
        }

        public List<Room> getRoomType (int id)
        {
            return myDb.rooms.Where(x => x.idType == id).ToList();
        }
    }
}