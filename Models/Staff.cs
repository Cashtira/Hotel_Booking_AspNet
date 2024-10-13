using System.ComponentModel.DataAnnotations;

namespace MVCmodel.Models
{
    public class Staff
    {
        public int StaffID { get; set; }
        public int HotelID { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Position { get; set; }
        public double Salary { get; set; }
        public DateTime DateOfBirth { get; set; }
        [Phone]
        public required  string Phone { get; set; }
        [EmailAddress]
        public required string Email { get; set; }
        public DateTime HireDate { get; set; }
    }
}
