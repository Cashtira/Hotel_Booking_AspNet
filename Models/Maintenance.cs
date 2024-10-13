namespace MVCmodel.Models
{
    public class Maintenance
    {
        public int MaintenanceID { get; set; }
        public int RoomID { get; set; }
        public string? IssueDescription { get; set; }
        public DateTime DateReported { get; set; }
        public DateTime RepairDate { get; set; }
        public required string Status { get; set; }

        public required  Room Room { get; set; }
    }
}
