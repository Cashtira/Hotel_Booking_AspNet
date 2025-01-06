using QuanLyKhachSan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;

namespace QuanLyKhachSan.Repositories
{
    public class ServiceRepository
    {
        private readonly QuanLyKhachSanDBContext _context;

        public ServiceRepository(QuanLyKhachSanDBContext context)
        {
            _context = context;
        }

        // Lấy tất cả dịch vụ
        public async Task<List<Service>> GetAllServicesAsync()
        {
            return await _context.Services.ToListAsync();
        }

        // Lấy các dịch vụ hàng đầu
        public async Task<List<Service>> GetTopServicesAsync(int limit = 5)
        {
            return await _context.Services.Take(limit).ToListAsync();
        }

        // Lấy giá dịch vụ theo ID
        public async Task<int> GetServiceCostByIdAsync(int serviceId)
        {
            var service = await _context.Services.FirstOrDefaultAsync(s => s.idService == serviceId);
            if (service == null)
                throw new ArgumentException($"No service found with id {serviceId}");

            return service.cost;
        }

        // Thêm dịch vụ mới
        public async Task AddServiceAsync(Service service)
        {
            if (service == null) throw new ArgumentNullException(nameof(service));

            _context.Services.Add(service);
            await SaveChangesAsync();
        }

        // Xóa dịch vụ theo ID
        public async Task DeleteServiceAsync(int serviceId)
        {
            var service = await _context.Services.FirstOrDefaultAsync(s => s.idService == serviceId);
            if (service == null) throw new ArgumentException($"No service found with id {serviceId}");

            _context.Services.Remove(service);
            await SaveChangesAsync();
        }

        // Cập nhật dịch vụ
        public async Task UpdateServiceAsync(Service updatedService)
        {
            if (updatedService == null) throw new ArgumentNullException(nameof(updatedService));

            var existingService = await _context.Services.FirstOrDefaultAsync(s => s.idService == updatedService.idService);
            if (existingService == null) throw new ArgumentException($"No service found with id {updatedService.idService}");

            existingService.name = updatedService.name;
            existingService.cost = updatedService.cost;
            await SaveChangesAsync();
        }

        // Lấy dịch vụ theo ID
        public async Task<Service> GetServiceByIdAsync(int serviceId)
        {
            var service = await _context.Services.FirstOrDefaultAsync(s => s.idService == serviceId);
            if (service == null) throw new ArgumentException($"No service found with id {serviceId}");

            return service;
        }

        // Lấy các dịch vụ trong booking
        public async Task<List<BookingService>> GetBookingServicesForServiceAsync(int serviceId)
        {
            return await _context.BookingServices.Where(bs => bs.idService == serviceId).ToListAsync();
        }

        private async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
