using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MVCmodel.Models
{
    public class Hotel
    {
        public int HotelID { get; set; }  // ID duy nhất của khách sạn

        public required string Name { get; set; } // Tên khách sạn, bắt buộc

        public required string Address { get; set; } // Địa chỉ khách sạn, bắt buộc

        [Phone]
        public required string Phone { get; set; } // Số điện thoại khách sạn, bắt buộc và phải là số điện thoại hợp lệ

        [EmailAddress]
        public required string Email { get; set; } // Địa chỉ email khách sạn, bắt buộc và phải là địa chỉ email hợp lệ

        [Range(1, 5)]
        public double Rating { get; set; } // Đánh giá của khách sạn từ 1 đến 5

        public TimeSpan CheckinTime { get; set; } // Thời gian nhận phòng

        public TimeSpan CheckoutTime { get; set; } // Thời gian trả phòng


    }
}
