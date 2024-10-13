using System.ComponentModel.DataAnnotations;

namespace MVCmodel.Models
{
    public class Hotel
    {
        public int HotelID { get; set; }
        public required string Name { get; set; }
        public required string Address { get; set; }
        [Phone]
        public required string Phone { get; set; }
        [EmailAddress]
        public required string Email { get; set; }
        [Range(1, 5)]
        public int Rating  { get; set; }
        public TimeSpan CheckinTime { get; set; }
        public TimeSpan CheckoutTime { get; set; }
    }
}
