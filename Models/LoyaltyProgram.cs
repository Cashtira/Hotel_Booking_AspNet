using System.ComponentModel.DataAnnotations;

namespace MVCmodel.Models
{
    public class LoyaltyProgram
    {
        [Key]
        public int ProgramID { get; set; }
        public int GuestID { get; set; }
        public int Points { get; set; }
        public string Tier { get; set; }

        public Guest Guest { get; set; }
    }
}
