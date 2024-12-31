using System.ComponentModel.DataAnnotations;

namespace MVCmodel.Models
{
    public class RoomType
    {
        [Key]
        public int TypeID { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public double PricePerNight { get; set; }
        public int Capacity { get; set; }
    }
}
    