using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2._Domain.Entities
{
    public  class Feedback
    {
        public int FeedbackId { get; set; }
        public required int UserId { get; set; }
        public required int HotelId { get; set; }
        public required float Rating { get; set; }
        public required string Comment { get; set; }

        public User User  { get; set; } = null!;
        public Hotel Hotel { get; set; } = null!;
    }
}
