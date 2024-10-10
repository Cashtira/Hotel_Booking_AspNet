namespace MVCmodel.Models
{
    public class Invoice
    {
        public int InvoiceID { get; set; }
        public int BookingID { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime IssuedDate { get; set; }
        public string PaidStatus { get; set; }

        public Booking Booking { get; set; }
    }
}
