using _2._Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2._Domain.Entities
{
    public class Invoice
    {
        public int InvoiceId { get; set; }
        public DateTime InvoiceTime { get; set; }
        public required int BookingId { get; set; }
        public PaymentMethod PaymentMethod { get; set; } = PaymentMethod.Cash;

        public Booking Booking { get; set; } = null!;
    }
}
