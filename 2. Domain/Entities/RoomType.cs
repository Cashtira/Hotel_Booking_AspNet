using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2._Domain.Entities
{
    public class RoomType
    {
        public int RoomTypeId { get; set; }
        public required string Name { get; set; }
        public required int Capacity { get; set; }
        public required decimal PricePerNight { get; set; }
        public decimal PricePerHour { get; set; }
        public string Description { get; set; } = string.Empty;

        public ICollection<Room> Rooms { get; set; } = new List<Room>();
    }
}
