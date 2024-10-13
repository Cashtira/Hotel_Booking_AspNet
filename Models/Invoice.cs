namespace MVCmodel.Models
{
    public class Invoice
    {
        public int InvoiceID { get; set; }
        public int BookingID { get; set; }
        public double TotalAmount { get; set; }
        public DateTime IssuedDate { get; set; }
        public required  string PaidStatus { get; set; }

        public required Booking Booking { get; set; }
    }
}
