using QuanLyKhachSan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace QuanLyKhachSan.Daos
{
    public class UserDao
    {

        QuanLyKhachSanDBContext myDb = new QuanLyKhachSanDBContext();
        public bool checkLogin(string userName, string password)
        {
            var obj = myDb.users.FirstOrDefault(x => x.userName == userName && x.password == password);
            if (obj == null) { return false; }
            return true;
        }

        public User getUserByUserName(string userName)
        {
            return myDb.users.FirstOrDefault(x => x.userName.Equals(userName));
        }

        public User getUserByEmail(string email)
        {
            return myDb.users.FirstOrDefault(x => x.email.Equals(email));
        }

        public User getInfor(int id)
        {
            return myDb.users.FirstOrDefault(x => x.idUser == id);
        }

        public List<User> getAdmin() { return myDb.users.Where(x => x.idRole == 1).ToList(); }

        public List<User> getNV() { return myDb.users.Where(x => x.idRole == 2).ToList(); }

        public List<User> getKH() { return myDb.users.Where(x => x.idRole == 3).ToList(); }

        public void add(User user)
        {
            myDb.users.Add(user);
            myDb.SaveChanges();
        }

        public bool checkExistUsername(string userName)
        {
            var obj = myDb.users.FirstOrDefault(x => x.userName == userName);
            if (obj != null) { return true; }
            return false;
        }

        public void delete(int id)
        {
            var obj = myDb.users.FirstOrDefault(x => x.idUser == id);
            myDb.users.Remove(obj);
            myDb.SaveChanges();
        }

        public void update(User user)
        {
            var obj = myDb.users.FirstOrDefault(x => x.idUser == user.idUser);
            obj.fullName = user.fullName;
            obj.userName = user.userName;
            obj.address = user.address;
            obj.phoneNumber = user.phoneNumber;
            obj.gender = user.gender;
            obj.email = user.email;
            myDb.SaveChanges();
        }

        public string md5(string password)
        {
            MD5 md = MD5.Create();
            byte[] inputString = System.Text.Encoding.ASCII.GetBytes(password);
            byte[] hash = md.ComputeHash(inputString);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("x"));
            }
            return sb.ToString();
        }

        public List<Booking> getCheck(int id)
        {
            return myDb.bookings.Where(x => x.idUser == id).ToList();
        }
    }
}