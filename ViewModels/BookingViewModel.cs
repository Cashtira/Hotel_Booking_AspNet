namespace MVCmodel.ViewModels
{
    public class BookingViewModel
    {
        public int BookingId { get; set; }
        public DateTime BookingTime { get; set; }
        public string UserName { get; set; }
        public DateTime CheckinDate { get; set; }
        public DateTime CheckoutDate { get; set; }
        public string Status { get; set; }
        public List<string> RoomNames { get; set; } = new List<string>();
        public List<string> ServiceNames { get; set; } = new List<string>();
    }

}
