using QuanLyKhachSan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;

namespace QuanLyKhachSan.Repositories
{
    public class RoomRepository
    {
        private readonly QuanLyKhachSanDBContext _context;

        public RoomRepository(QuanLyKhachSanDBContext context)
        {
            _context = context;
        }

        // Lấy danh sách tất cả các phòng
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
                .OrderBy(x => x.idRoom) // Thêm sắp xếp để đảm bảo Skip hoạt động
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
                .OrderBy(x => x.idRoom) // Thêm sắp xếp
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }


        // Tìm kiếm phòng theo loại phòng và tên
        public async Task<List<Room>> SearchRoomsByTypeAndNameAsync(int page, int pageSize, int idType, string name, int numberChildren, int numberAdult)
        {
            var bookedRoomIds = await _context.Bookings
                .Where(x => x.status == 0 || x.status == 1)
                .Select(x => x.idRoom)
                .Distinct()
                .ToListAsync();

            return await _context.Rooms
                .Where(x => !bookedRoomIds.Contains(x.idRoom) &&
                            x.idType == idType &&
                            x.name.Contains(name) &&
                            x.numberAdult >= numberAdult &&
                            x.numberChildren >= numberChildren)
                .OrderBy(x => x.idRoom) // Thêm sắp xếp
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<List<Room>> SearchRoomsByTypeAsync(int page, int pageSize, int idType, int numberChildren, int numberAdult)
        {
            var bookedRoomIds = await _context.Bookings
                .Where(x => x.status == 0 || x.status == 1)
                .Select(x => x.idRoom)
                .Distinct()
                .ToListAsync();

            return await _context.Rooms
                .Where(x => !bookedRoomIds.Contains(x.idRoom) &&
                            x.idType == idType &&
                            x.numberAdult >= numberAdult &&
                            x.numberChildren >= numberChildren)
                .OrderBy(x => x.idRoom) // Thêm sắp xếp
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

        // Lấy tổng số nhóm phòng còn trống (mỗi nhóm 3 phòng)
        public async Task<int> GetTotalAvailableRoomGroupsAsync()
        {
            var bookedRoomIds = await _context.Bookings
                                     .Where(x => x.status == 0 || x.status == 1)
                                     .Select(x => x.idRoom)
                                     .Distinct()
                                     .ToListAsync();

            var allRoomIds = await _context.Rooms.Select(x => x.idRoom).ToListAsync();

            var availableRoomIds = allRoomIds.Except(bookedRoomIds).ToList();

            int totalAvailableRooms = await _context.Rooms.CountAsync(x => availableRoomIds.Contains(x.idRoom));

            int roomGroups = totalAvailableRooms / 3;
            if (totalAvailableRooms % 3 != 0)
            {
                roomGroups++;
            }

            return roomGroups;
        }
        // Lấy tổng số nhóm phòng trống theo loại phòng
        public async Task<int> GetTotalAvailableRoomGroupsByTypeAsync(int idType, int numberChildren, int numberAdult)
        {
            // Lấy danh sách id phòng đã được đặt
            var bookedRoomIds = await _context.Bookings
                .Where(x => x.status == 0 || x.status == 1)
                .Select(x => x.idRoom)
                .Distinct()
                .ToListAsync();

            // Lấy danh sách id phòng còn trống
            var availableRoomIds = await _context.Rooms
                .Where(x => !bookedRoomIds.Contains(x.idRoom) &&
                            x.idType == idType &&
                            x.numberAdult >= numberAdult &&
                            x.numberChildren >= numberChildren)
                .Select(x => x.idRoom)
                .ToListAsync();

            // Tính tổng số nhóm phòng (mỗi nhóm 3 phòng)
            int totalRooms = availableRoomIds.Count;
            int roomGroups = totalRooms / 3;
            if (totalRooms % 3 != 0)
            {
                roomGroups++;
            }

            return roomGroups;
        }
        public async Task<int> GetTotalAvailableRoomGroupsByNameAndTypeAsync(string name, int idType, int numberChildren, int numberAdult)
        {
            // Lấy danh sách id phòng đã được đặt
            var bookedRoomIds = await _context.Bookings
                .Where(x => x.status == 0 || x.status == 1)
                .Select(x => x.idRoom)
                .Distinct()
                .ToListAsync();

            // Lấy danh sách id phòng còn trống
            var availableRoomIds = await _context.Rooms
                .Where(x => !bookedRoomIds.Contains(x.idRoom) &&
                            x.name.Contains(name) &&
                            x.idType == idType &&
                            x.numberAdult >= numberAdult &&
                            x.numberChildren >= numberChildren)
                .Select(x => x.idRoom)
                .ToListAsync();

            // Tính tổng số nhóm phòng (mỗi nhóm 3 phòng)
            int totalRooms = availableRoomIds.Count;
            int roomGroups = totalRooms / 3;
            if (totalRooms % 3 != 0)
            {
                roomGroups++;
            }

            return roomGroups;
        }

        // Lấy tổng số nhóm phòng trống theo tên phòng
        public async Task<int> GetTotalAvailableRoomGroupsByNameAsync(string name, int numberChildren, int numberAdult)
        {
            // Lấy danh sách id phòng đã được đặt
            var bookedRoomIds = await _context.Bookings
                .Where(x => x.status == 0 || x.status == 1)
                .Select(x => x.idRoom)
                .Distinct()
                .ToListAsync();

            // Lấy danh sách id phòng còn trống
            var availableRoomIds = await _context.Rooms
                .Where(x => !bookedRoomIds.Contains(x.idRoom) &&
                            x.name.Contains(name) &&
                            x.numberAdult >= numberAdult &&
                            x.numberChildren >= numberChildren)
                .Select(x => x.idRoom)
                .ToListAsync();

            // Tính tổng số nhóm phòng (mỗi nhóm 3 phòng)
            int totalRooms = availableRoomIds.Count;
            int roomGroups = totalRooms / 3;
            if (totalRooms % 3 != 0)
            {
                roomGroups++;
            }

            return roomGroups;
        }

        // Kiểm tra xem phòng có đơn đặt chỗ hay không
        public async Task<bool> HasBookingAsync(int roomId)
        {
            return await _context.Bookings.AnyAsync(b => b.idRoom == roomId);
        }
    }
}
