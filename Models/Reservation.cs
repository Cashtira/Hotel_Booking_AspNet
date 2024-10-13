namespace MVCmodel.Models
{
    public class Reservation
    {
        public int ReservationID { get; set; }
        public int GuestID { get; set; }
        public int RoomID { get; set; }
        public required DateTime ReservationDate { get; set; }
        public required string Status { get; set; }

        public required Guest Guest { get; set; }
        public  required Room Room { get; set; }
    }
}
