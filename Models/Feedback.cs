namespace MVCmodel.Models
{
    public class Feedback
    {
        public int FeedbackID { get; set; }
        public int GuestID { get; set; }
        public int HotelID { get; set; }
        public int Rating { get; set; }
        public string Comments { get; set; }

        public Guest Guest { get; set; }
        public Hotel Hotel { get; set; }
    }
}
