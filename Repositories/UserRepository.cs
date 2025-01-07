using QuanLyKhachSan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

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
        public async Task<bool> CheckLoginAsync(string userName, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.userName == userName);
            if (user == null) return false;

            return user.password == password;
        }

        // Lấy thông tin người dùng theo tên đăng nhập
        public async Task<User> GetUserByUserNameAsync(string userName)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.userName.Equals(userName));
        }

        // Lấy thông tin người dùng theo email
        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.email.Equals(email));
        }

        // Lấy thông tin người dùng theo ID
        public async Task<User> GetUserInfoAsync(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.idUser == id);
        }

        // Lấy danh sách quản trị viên
        public async Task<List<User>> GetAdminsAsync()
        {
            return await _context.Users.Where(x => x.idRole == 1).ToListAsync();
        }

        // Lấy danh sách nhân viên
        public async Task<List<User>> GetEmployeesAsync()
        {
            return await _context.Users.Where(x => x.idRole == 2).ToListAsync();
        }

        // Lấy danh sách khách hàng
        public async Task<List<User>> GetCustomersAsync()
        {
            return await _context.Users.Where(x => x.idRole == 3).ToListAsync();
        }

        // Thêm người dùng mới
        public async Task AddUserAsync(User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        // Kiểm tra tồn tại username
        public async Task<bool> CheckUsernameExistenceAsync(string userName)
        {
            return await _context.Users.AnyAsync(x => x.userName == userName);
        }

        // Xóa người dùng
        public async Task DeleteUserAsync(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.idUser == id);
            if (user == null) throw new ArgumentException($"No user found with id {id}");

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        // Cập nhật thông tin người dùng
        public async Task UpdateUserAsync(User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            var existingUser = await _context.Users.FirstOrDefaultAsync(x => x.idUser == user.idUser);
            if (existingUser == null) throw new ArgumentException($"No user found with id {user.idUser}");

            existingUser.fullName = user.fullName;
            existingUser.userName = user.userName;
            existingUser.address = user.address;
            existingUser.phoneNumber = user.phoneNumber;
            existingUser.gender = user.gender;
            existingUser.email = user.email;
            await _context.SaveChangesAsync();
        }

        // Mã hóa mật khẩu với MD5
        public string Md5Hash(string password)
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

        // Lấy danh sách đặt phòng của người dùng
        public async Task<List<Booking>> GetBookingsForUserAsync(int userId)
        {
            return await _context.Bookings.Where(x => x.idUser == userId).ToListAsync();
        }


        public async Task<bool> HasBookingsAsync(int userId)
        {
            // Kiểm tra xem người dùng có đặt phòng nào không
            return await _context.Bookings.AnyAsync(x => x.idUser == userId);
        }

    }
}
