namespace MVCmodel.Models
{
    public class Hotel
    {
        public int HotelID { get; set; }
        public required string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int Stars { get; set; }
        public TimeSpan CheckinTime { get; set; }
        public TimeSpan CheckoutTime { get; set; }
    }
}
