using System.ComponentModel.DataAnnotations;

namespace MVCmodel.DTOs
{
    public class HotelDto
    {
        public int HotelID { get; set; }  // ID duy nhất của khách sạn

        [Required(ErrorMessage = "Tên khách sạn là bắt buộc.")]
        public string Name { get; set; } // Tên khách sạn, bắt buộc

        [Required(ErrorMessage = "Địa chỉ khách sạn là bắt buộc.")]
        public string Address { get; set; } // Địa chỉ khách sạn, bắt buộc

        [Required(ErrorMessage = "Số điện thoại khách sạn là bắt buộc.")]
        [Phone(ErrorMessage = "Số điện thoại không hợp lệ.")]
        public string Phone { get; set; } // Số điện thoại khách sạn

        [Required(ErrorMessage = "Địa chỉ email khách sạn là bắt buộc.")]
        [EmailAddress(ErrorMessage = "Địa chỉ email không hợp lệ.")]
        public string Email { get; set; } // Địa chỉ email khách sạn

        [Range(1, 5, ErrorMessage = "Đánh giá phải nằm trong khoảng từ 1 đến 5.")]
        public double Rating { get; set; } // Đánh giá của khách sạn

        public TimeSpan CheckinTime { get; set; } // Thời gian nhận phòng

        public TimeSpan CheckoutTime { get; set; } // Thời gian trả phòng
    }
}
