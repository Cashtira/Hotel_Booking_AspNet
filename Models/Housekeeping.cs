namespace MVCmodel.Models
{
    public class Housekeeping
    {
        public int HousekeepingID { get; set; }
        public int RoomID { get; set; }
        public int StaffID { get; set; }
        public DateTime DateCleaned { get; set; }
        public string Status { get; set; }

        public Room Room { get; set; }
        public Staff Staff { get; set; }
    }
}
