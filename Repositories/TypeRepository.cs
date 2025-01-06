using QuanLyKhachSan.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Type = QuanLyKhachSan.Models.Type;

namespace QuanLyKhachSan.Repositories
{
    public class TypeRepository
    {
        private readonly QuanLyKhachSanDBContext _context;

        public TypeRepository(QuanLyKhachSanDBContext context)
        {
            _context = context;
        }

        public async Task<List<Type>> GetAllTypesAsync()
        {
            return await _context.Types.ToListAsync();
        }

        public async Task AddTypeAsync(Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));

            _context.Types.Add(type);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTypeAsync(int id)
        {
            var type = await _context.Types.FirstOrDefaultAsync(x => x.idType == id);
            if (type == null) throw new ArgumentException($"No type found with id {id}");

            _context.Types.Remove(type);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTypeAsync(QuanLyKhachSan.Models.Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));

            var existingType = await _context.Types.FirstOrDefaultAsync(x => x.idType == type.idType);
            if (existingType == null) throw new ArgumentException($"No type found with id {type.idType}");

            existingType.name = type.name;
            await _context.SaveChangesAsync();
        }

        public async Task<QuanLyKhachSan.Models.Type> GetTypeByIdAsync(int id)
        {
            return await _context.Types.FirstOrDefaultAsync(x => x.idType == id);
        }

        public async Task<List<Room>> GetRoomsByTypeAsync(int id)
        {
            return await _context.Rooms.Where(x => x.idType == id).ToListAsync();
        }
    }
}
