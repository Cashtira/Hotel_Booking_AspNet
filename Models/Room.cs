namespace MVCmodel.Models
{
    public class Room
    {
        public int RoomID { get; set; }
        public int HotelID { get; set; }
        public int TypeID { get; set; }
        public string Status { get; set; }

        public Hotel Hotel { get; set; }
        public RoomType RoomType { get; set; }
    }
}
