namespace MVCmodel.Models
{
    public class Booking
    {
        public int BookingID { get; set; }
        public int GuestID { get; set; }
        public int RoomID { get; set; }
        public DateTime CheckinDate { get; set; }
        public DateTime CheckoutDate { get; set; }
        public decimal TotalPrice { get; set; }

        public Guest Guest { get; set; }
        public Room Room { get; set; }
    }
}
