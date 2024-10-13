using MVCmodel.Models;

public class Booking
{
    public int BookingID { get; set; }
    public int GuestID { get; set; }
    public int RoomID { get; set; }

    public required DateTime CheckinDate { get; set; }
    public required DateTime CheckoutDate { get; set; }

    public double TotalPrice { get; set; }
    public required Guest Guest { get; set; }
    public required Room Room { get; set; }
}
