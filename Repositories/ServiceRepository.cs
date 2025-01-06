using QuanLyKhachSan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public List<Service> GetAllServices()
        {
            return _context.Services.ToList();
        }

        public List<Service> GetTopServices(int limit = 5)
        {
            return _context.Services.Take(limit).ToList();
        }

        public int GetServiceCostById(int serviceId)
        {
            var service = _context.Services.FirstOrDefault(s => s.idService == serviceId);
            if (service == null)
                throw new ArgumentException($"No service found with id {serviceId}");

            return service.cost;
        }

        // Thêm dịch vụ mới
        public void AddService(Service service)
        {
            if (service == null) throw new ArgumentNullException(nameof(service));

            _context.Services.Add(service);
            SaveChanges();
        }

        public void DeleteService(int serviceId)
        {
            var service = _context.Services.FirstOrDefault(s => s.idService == serviceId);
            if (service == null) throw new ArgumentException($"No service found with id {serviceId}");

            _context.Services.Remove(service);
            SaveChanges();
        }

        public void UpdateService(Service updatedService)
        {
            if (updatedService == null) throw new ArgumentNullException(nameof(updatedService));

            var existingService = _context.Services.FirstOrDefault(s => s.idService == updatedService.idService);
            if (existingService == null) throw new ArgumentException($"No service found with id {updatedService.idService}");

            existingService.name = updatedService.name;
            existingService.cost = updatedService.cost;
            SaveChanges();
        }

        public Service GetServiceById(int serviceId)
        {
            var service = _context.Services.FirstOrDefault(s => s.idService == serviceId);
            if (service == null) throw new ArgumentException($"No service found with id {serviceId}");

            return service;
        }

        public List<BookingService> GetBookingServicesForService(int serviceId)
        {
            return _context.BookingServices.Where(bs => bs.idService == serviceId).ToList();
        }

        private void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
