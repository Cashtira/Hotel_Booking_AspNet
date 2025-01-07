using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyKhachSan.ViewModel
{
    public class StatisticViewModel
    {
        public int UserCount { get; set; }
        public int BookingCount { get; set; }
        public int BookingCompletedCount { get; set; }
        public int BookingCancelledCount { get; set; }
        public int BookingsToday { get; set; }
        public int UpcomingBookings { get; set; }
        public int TotalRevenue { get; set; }
        public int AverageRevenuePerBooking { get; set; }
        public List<int> MonthlyData { get; set; } = new List<int>(new int[12]); // Khởi tạo với giá trị mặc định
    }
}