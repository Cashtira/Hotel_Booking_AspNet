namespace MVCmodel.Models
{
    public class Room
    {
        public int RoomID { get; set; }
        public int HotelID { get; set; }
        public int TypeID { get; set; }
        public required string Status { get; set; }
        public required string RoomNumber { get; set; }  // Thêm thuộc tính RoomNumber
        public required  Hotel Hotel { get; set; }
        public required  RoomType RoomType { get; set; }
    }
}
