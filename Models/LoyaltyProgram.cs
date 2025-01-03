using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCmodel.Models
{
    [Table(nameof(LoyaltyProgram))]
    public sealed class LoyaltyProgram
    {
        [Key]
        public int LoyaltyProgramId { get; set; }

        public required string UserId { get; set; }

        public required int Points { get; set; }

        public required int Tier { get; set; }

        [ForeignKey(nameof(UserId))]
        [InverseProperty(nameof(User.LoyaltyPrograms))]
        public User User { get; set; } = null!;
    }
}
