using QuanLyKhachSan.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyKhachSan.Repositories
{
    public class RoomCommentRepository
    {
        private readonly QuanLyKhachSanDBContext _context;

        public RoomCommentRepository(QuanLyKhachSanDBContext context)
        {
            _context = context;
        }

        public async Task AddRoomCommentAsync(RoomComment roomComment)
        {
            _context.RoomComments.Add(roomComment);
            await _context.SaveChangesAsync();
        }

        public async Task<List<RoomComment>> GetRoomCommentsByRoomIdAsync(int roomId)
        {
            return await _context.RoomComments
                .Where(x => x.idRoom == roomId)
                .OrderByDescending(x => x.createdDate)
                .ToListAsync();
        }

        public async Task<double> GetAverageStarByRoomIdAsync(int roomId)
        {
            var average = await _context.RoomComments
                .Where(x => x.idRoom == roomId)
                .Select(x => (double?)x.star) 
                .AverageAsync();

            return average ?? 5; 
        }
    }
}
