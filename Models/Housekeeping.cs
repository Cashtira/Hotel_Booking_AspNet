namespace MVCmodel.Models
{
    public class Housekeeping
    {
        public int HousekeepingID { get; set; }
        public int RoomID { get; set; }
        public int StaffID { get; set; }
        public required DateTime DateCleaned { get; set; }
        public required string Status { get; set; }

        public required Room Room { get; set; }
        public required Staff Staff { get; set; }
    }
}
