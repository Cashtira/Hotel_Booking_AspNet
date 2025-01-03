
using QuanLyKhachSan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyKhachSan.Daos
{
    public class RoomDao
    {
        QuanLyKhachSanDBContext myDb = new QuanLyKhachSanDBContext();


        public List<Room> GetRooms()
        {
            return myDb.rooms.ToList();
        }

        public List<Room> GetRoomTop5()
        {
            return myDb.rooms.OrderByDescending(x => x.view).Take(3).ToList();
        }

        public List<Room> GetRoomDiscount()
        {
            return myDb.rooms.Where(x => x.discount > 0).OrderByDescending(x => x.discount).Take(3).ToList();
        }

        public Room GetDetail(int id)
        {
            return myDb.rooms.FirstOrDefault(x => x.idRoom == id);
        }

        public List<Room> GetRoomByType(int typeId)
        {
            return myDb.rooms.Where(x => x.idType == typeId).ToList();
        }

        public List<Room> GetRoomsBlank(int page, int pagesize)
        {
            var arrIdRoom = myDb.bookings.Where(x => x.status == 0 || x.status == 1).Select(x => x.idRoom).Distinct().ToList();
            var allId = myDb.rooms.Select(x => x.idRoom).ToList();
            var ids = allId.Except(arrIdRoom).ToList();
            return myDb.rooms.Where(x => ids.Contains(x.idRoom)).ToList().Skip((page - 1) * pagesize).Take(pagesize).ToList();
        }
        public int GetNumberRoom()
        {
            var arrIdRoom = myDb.bookings.Where(x => x.status == 0 || x.status == 1).Select(x => x.idRoom).Distinct().ToList();
            var allId = myDb.rooms.Select(x => x.idRoom).ToList();
            var ids = allId.Except(arrIdRoom).ToList();
            int total = myDb.rooms.Where(x => ids.Contains(x.idRoom)).ToList().Count;
            int count = 0;
            count = total / 3;
            if (total % 3 != 0)
            {
                count++;
            }
            return count;
        }

        public List<Room> SearchByName(int page, int pagesize,string name, int numberChildren, int numberAdult)
        {
            var arrIdRoom = myDb.bookings.Where(x => x.status == 0 || x.status == 1).Select(x => x.idRoom).Distinct().ToList();
            var allId = myDb.rooms.Select(x => x.idRoom).ToList();
            var ids = allId.Except(arrIdRoom).ToList();
            return myDb.rooms.Where(x => ids.Contains(x.idRoom) && x.name.Contains(name) && x.numberAdult >= numberAdult && x.numberChildren >= numberChildren).ToList().Skip((page - 1) * pagesize).Take(pagesize).ToList();
        }

        public List<Room> SearchByType(int page, int pagesize,int idType,int numberChildren, int numberAdult)
        {
            var arrIdRoom = myDb.bookings.Where(x => x.status == 0 || x.status == 1).Select(x => x.idRoom).Distinct().ToList();
            var allId = myDb.rooms.Select(x => x.idRoom).ToList();
            var ids = allId.Except(arrIdRoom).ToList();
            return myDb.rooms.Where(x => ids.Contains(x.idRoom) && x.idType == idType && x.numberAdult >= numberAdult && x.numberChildren >= numberChildren).ToList().Skip((page - 1) * pagesize).Take(pagesize).ToList();
        }

        public int GetNumberRoomByType(int idType, int numberChildren, int numberAdult)
        {
            var arrIdRoom = myDb.bookings.Where(x => x.status == 0 || x.status == 1).Select(x => x.idRoom).Distinct().ToList();
            var allId = myDb.rooms.Select(x => x.idRoom).ToList();
            var ids = allId.Except(arrIdRoom).ToList();
            int total = myDb.rooms.Where(x => ids.Contains(x.idRoom) && x.idType == idType && x.numberAdult >= numberAdult && x.numberChildren >= numberChildren).ToList().Count;
            int count = 0;
            count = total / 3;
            if (total % 3 != 0)
            {
                count++;
            }
            return count;
        }

        public int GetNumberRoomByName(string name, int numberChildren, int numberAdult)
        {
            var arrIdRoom = myDb.bookings.Where(x => x.status == 0 || x.status == 1).Select(x => x.idRoom).Distinct().ToList();
            var allId = myDb.rooms.Select(x => x.idRoom).ToList();
            var ids = allId.Except(arrIdRoom).ToList();
            int total = myDb.rooms.Where(x => ids.Contains(x.idRoom) && x.name.Contains(name) && x.numberAdult >= numberAdult && x.numberChildren >= numberChildren).ToList().Count;
            int count = 0;
            count = total / 3;
            if (total % 3 != 0)
            {
                count++;
            }
            return count;
        }

        public List<Room> SearchByTypeAndName(int page, int pagesize, int idType,string name, int numberChildren, int numberAdult)
        {
            var arrIdRoom = myDb.bookings.Where(x => x.status == 0 || x.status == 1).Select(x => x.idRoom).Distinct().ToList();
            var allId = myDb.rooms.Select(x => x.idRoom).ToList();
            var ids = allId.Except(arrIdRoom).ToList();
            return myDb.rooms.Where(x => ids.Contains(x.idRoom) && x.idType == idType && x.name.Contains(name) && x.numberAdult >= numberAdult && x.numberChildren >= numberChildren).ToList().Skip((page - 1) * pagesize).Take(pagesize).ToList();
        }

        public int GetNumberRoomByNameAndType(string name, int idType, int numberChildren, int numberAdult)
        {
            var arrIdRoom = myDb.bookings.Where(x => x.status == 0 || x.status == 1).Select(x => x.idRoom).Distinct().ToList();
            var allId = myDb.rooms.Select(x => x.idRoom).ToList();
            var ids = allId.Except(arrIdRoom).ToList();
            int total = myDb.rooms.Where(x => ids.Contains(x.idRoom) && x.name.Contains(name) && x.idType == idType && x.numberAdult >= numberAdult && x.numberChildren >= numberChildren).ToList().Count;
            int count = 0;
            count = total / 3;
            if (total % 3 != 0)
            {
                count++;
            }
            return count;
        }

        public void add(Room room)
        {
            myDb.rooms.Add(room);
            myDb.SaveChanges();
        }

        public void delete(int id)
        {
            var obj = myDb.rooms.FirstOrDefault(x => x.idRoom == id);
            myDb.rooms.Remove(obj);
            myDb.SaveChanges();
        }

        public void update(Room room)
        {
            var obj = myDb.rooms.FirstOrDefault(x => x.idRoom == room.idRoom);
            obj.name = room.name;
            obj.image = room.image;
            obj.description = room.description;
            obj.discount = room.discount;
            obj.cost = room.cost;
            obj.idType = room.idType;
            obj.numberChildren = room.numberChildren;
            obj.numberAdult = room.numberAdult;
            myDb.SaveChanges();
        }

        public void updateView(int id)
        {
            var obj = myDb.rooms.FirstOrDefault(x => x.idRoom == id);
            obj.view = obj.view + 1;
            myDb.SaveChanges();
        }

        public List<Booking> getCheck(int id)
        {
            return myDb.bookings.Where(x => x.idRoom == id).ToList();
        }
    }
}