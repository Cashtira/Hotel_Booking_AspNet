using QuanLyKhachSan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhachSan.Repositories
{
    public class UserRepository
    {
        private readonly QuanLyKhachSanDBContext _context;

        public UserRepository(QuanLyKhachSanDBContext context)
        {
            _context = context;
        }

        // Kiểm tra đăng nhập
        public bool CheckLogin(string userName, string password)
        {
            var user = _context.Users.FirstOrDefault(x => x.userName == userName);
            if (user == null) return false;

            return user.password == Md5Hash(password);
        }

        // Lấy thông tin người dùng theo tên đăng nhập
        public User GetUserByUserName(string userName)
        {
            return _context.Users.FirstOrDefault(x => x.userName.Equals(userName));
        }

        // Lấy thông tin người dùng theo email
        public User GetUserByEmail(string email)
        {
            return _context.Users.FirstOrDefault(x => x.email.Equals(email));
        }

        // Lấy thông tin người dùng theo ID
        public User GetUserInfo(int id)
        {
            return _context.Users.FirstOrDefault(x => x.idUser == id);
        }

        // Lấy danh sách quản trị viên
        public List<User> GetAdmins()
        {
            return _context.Users.Where(x => x.idRole == 1).ToList();
        }

        // Lấy danh sách nhân viên
        public List<User> GetEmployees()
        {
            return _context.Users.Where(x => x.idRole == 2).ToList();
        }

        // Lấy danh sách khách hàng
        public List<User> GetCustomers()
        {
            return _context.Users.Where(x => x.idRole == 3).ToList();
        }

        // Thêm người dùng mới
        public void AddUser(User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            _context.Users.Add(user);
            SaveChanges();
        }

        // Kiểm tra tồn tại username
        public bool CheckUsernameExistence(string userName)
        {
            return _context.Users.Any(x => x.userName == userName);
        }

        // Xóa người dùng
        public void DeleteUser(int id)
        {
            var user = _context.Users.FirstOrDefault(x => x.idUser == id);
            if (user == null) throw new ArgumentException($"No user found with id {id}");

            _context.Users.Remove(user);
            SaveChanges();
        }

        // Cập nhật thông tin người dùng
        public void UpdateUser(User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            var existingUser = _context.Users.FirstOrDefault(x => x.idUser == user.idUser);
            if (existingUser == null) throw new ArgumentException($"No user found with id {user.idUser}");

            existingUser.fullName = user.fullName;
            existingUser.userName = user.userName;
            existingUser.address = user.address;
            existingUser.phoneNumber = user.phoneNumber;
            existingUser.gender = user.gender;
            existingUser.email = user.email;
            SaveChanges();
        }

        // Mã hóa mật khẩu với MD5
        public string Md5Hash(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                StringBuilder sb = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    sb.Append(b.ToString("x2"));
                }
                return sb.ToString();
            }
        }

        public List<Booking> GetBookingsForUser(int userId)
        {
            return _bookingRepository.Bookings.Where(x => x.idUser == userId).ToList();
        }

        private void SaveChanges()
        {
            _bookingRepository.SaveChanges();
        }
    }
}
