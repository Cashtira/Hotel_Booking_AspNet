namespace MVCmodel.Models
{
    public class Payment
    {
        public int PaymentID { get; set; }
        public int BookingID { get; set; }
        public double Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public required string PaymentMethod { get; set; }

        public  required Booking Booking { get; set; }
    }
}
