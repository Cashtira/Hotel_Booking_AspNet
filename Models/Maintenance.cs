using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCmodel.Models
{
    [Table(nameof(Maintenance))]
    public sealed class Maintenance
    {
        [Key]
        public int MaintenanceId { get; set; }

        public required int RoomId { get; set; }

        [StringLength(200)]
        public required string IssueDescription { get; set; }

        public required DateTimeOffset ReportTime { get; set; }

        public DateTimeOffset? RepairDate { get; set; } = null;

        [ForeignKey(nameof(RoomId))]
        [InverseProperty(nameof(Room.Maintenances))]
        public Room Room { get; set; } = null!;
    }
}
