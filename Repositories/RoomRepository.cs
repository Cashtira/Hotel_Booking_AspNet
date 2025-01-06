using QuanLyKhachSan.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyKhachSan.Repositories
{
    public class RoomRepository
    {
        private readonly QuanLyKhachSanDBContext _context;

        public RoomRepository(QuanLyKhachSanDBContext context)
        {
            _context = context;
        }

        public async Task<List<Room>> GetRoomsAsync()
        {
            return await _context.Rooms.ToListAsync();
        }

        // Lấy top 3 phòng theo lượt xem
        public async Task<List<Room>> GetTopRoomsByViewAsync(int top = 3)
        {
            return await _context.Rooms
                .OrderByDescending(x => x.view)
                .Take(top)
                .ToListAsync();
        }

        // Lấy phòng có giảm giá cao nhất
        public async Task<List<Room>> GetDiscountedRoomsAsync(int top = 3)
        {
            return await _context.Rooms
                .Where(x => x.discount > 0)
                .OrderByDescending(x => x.discount)
                .Take(top)
                .ToListAsync();
        }

        // Lấy chi tiết phòng
        public async Task<Room> GetRoomDetailsAsync(int roomId)
        {
            return await _context.Rooms.FirstOrDefaultAsync(x => x.idRoom == roomId);
        }

        // Lấy danh sách phòng theo loại
        public async Task<List<Room>> GetRoomsByTypeAsync(int typeId)
        {
            return await _context.Rooms.Where(x => x.idType == typeId).ToListAsync();
        }

        // Lấy danh sách phòng trống (có phân trang)
        public async Task<List<Room>> GetAvailableRoomsAsync(int page, int pageSize)
        {
            var bookedRoomIds = await _context.Bookings
                .Where(x => x.status == 0 || x.status == 1)
                .Select(x => x.idRoom)
                .Distinct()
                .ToListAsync();

            return await _context.Rooms
                .Where(x => !bookedRoomIds.Contains(x.idRoom))
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        // Tìm kiếm phòng theo tên và các tiêu chí
        public async Task<List<Room>> SearchRoomsByNameAsync(int page, int pageSize, string name, int numberChildren, int numberAdult)
        {
            var bookedRoomIds = await _context.Bookings
                .Where(x => x.status == 0 || x.status == 1)
                .Select(x => x.idRoom)
                .Distinct()
                .ToListAsync();

            return await _context.Rooms
                .Where(x => !bookedRoomIds.Contains(x.idRoom) &&
                            x.name.Contains(name) &&
                            x.numberAdult >= numberAdult &&
                            x.numberChildren >= numberChildren)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        // Thêm phòng
        public async Task AddRoomAsync(Room room)
        {
            _context.Rooms.Add(room);
            await _context.SaveChangesAsync();
        }

        // Xóa phòng
        public async Task DeleteRoomAsync(int roomId)
        {
            var room = await _context.Rooms.FirstOrDefaultAsync(x => x.idRoom == roomId);
            if (room != null)
            {
                _context.Rooms.Remove(room);
                await _context.SaveChangesAsync();
            }
        }

        // Cập nhật phòng
        public async Task UpdateRoomAsync(Room room)
        {
            var existingRoom = await _context.Rooms.FirstOrDefaultAsync(x => x.idRoom == room.idRoom);
            if (existingRoom != null)
            {
                existingRoom.name = room.name;
                existingRoom.image = room.image;
                existingRoom.description = room.description;
                existingRoom.discount = room.discount;
                existingRoom.cost = room.cost;
                existingRoom.idType = room.idType;
                existingRoom.numberChildren = room.numberChildren;
                existingRoom.numberAdult = room.numberAdult;
                await _context.SaveChangesAsync();
            }
        }

        // Cập nhật lượt xem phòng
        public async Task UpdateRoomViewAsync(int roomId)
        {
            var room = await _context.Rooms.FirstOrDefaultAsync(x => x.idRoom == roomId);
            if (room != null)
            {
                room.view++;
                await _context.SaveChangesAsync();
            }
        }
    }
}
