using QuanLyKhachSan.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuanLyKhachSan.Repositories
{
    public class TypeRepository
    {
        private readonly QuanLyKhachSanDBContext _context;

        public TypeRepository(QuanLyKhachSanDBContext context)
        {
            _context = context;
        }

        // Lấy danh sách tất cả các loại phòng
        public List<QuanLyKhachSan.Models.Type> GetAllTypes()
        {
            return _context.Types.ToList();
        }

        // Thêm loại phòng mới
        public void AddType(QuanLyKhachSan.Models.Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));

            _context.Types.Add(type);
            SaveChanges();
        }

        // Xóa loại phòng theo id
        public void DeleteType(int id)
        {
            var type = _context.Types.FirstOrDefault(x => x.idType == id);
            if (type == null) throw new ArgumentException($"No type found with id {id}");

            _context.Types.Remove(type);
            SaveChanges();
        }

        // Cập nhật thông tin loại phòng
        public void UpdateType(QuanLyKhachSan.Models.Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));

            var existingType = _context.Types.FirstOrDefault(x => x.idType == type.idType);
            if (existingType == null) throw new ArgumentException($"No type found with id {type.idType}");

            existingType.name = type.name;
            SaveChanges();
        }

        // Lấy thông tin loại phòng theo id
        public QuanLyKhachSan.Models.Type GetTypeById(int id)
        {
            return _context.Types.FirstOrDefault(x => x.idType == id);
        }

        // Lấy danh sách các phòng theo loại phòng
        public List<Room> GetRoomsByType(int id)
        {
            return _context.Rooms.Where(x => x.idType == id).ToList();
        }

        // Lưu thay đổi vào cơ sở dữ liệu
        private void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
