using System.ComponentModel.DataAnnotations;

namespace MVCmodel.Models
{
    public class LoyaltyProgram
    {
        [Key]
        public int ProgramID { get; set; }
        public int GuestID { get; set; }
        public int Points { get; set; }
        public required string Tier { get; set; }

        public required Guest Guest { get; set; }
    }
}
