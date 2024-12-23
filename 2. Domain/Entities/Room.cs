using _2._Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2._Domain.Entities
{
    public class Room
    {
        public int RoomId { get; set; }
        public required string Name { get; set; }
        public required int HotelId { get; set; }
        public required int RoomTypeId { get; set; }
        public RoomStatus Status { get; set; } = RoomStatus.Available;

        public RoomType RoomType { get; set; } = null!;
        public Hotel Hotel { get; set; } = null!;
    }
}
