namespace MVCmodel.Models
{
    public class Maintenance
    {
        public int MaintenanceID { get; set; }
        public int RoomID { get; set; }
        public string IssueDescription { get; set; }
        public DateTime DateReported { get; set; }
        public DateTime RepairDate { get; set; }
        public string Status { get; set; }

        public Room Room { get; set; }
    }
}
