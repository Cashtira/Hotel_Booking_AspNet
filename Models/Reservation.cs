namespace MVCmodel.Models
{
    public class Reservation
    {
        public int ReservationID { get; set; }
        public int GuestID { get; set; }
        public int RoomID { get; set; }
        public DateTime ReservationDate { get; set; }
        public string Status { get; set; }

        public Guest Guest { get; set; }
        public Room Room { get; set; }
    }
}
