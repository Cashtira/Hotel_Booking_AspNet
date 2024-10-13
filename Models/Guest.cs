using System.ComponentModel.DataAnnotations;

public class Guest
{
    public int GuestID { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public DateTime? DateOfBirth { get; set; }
   
    public string? Address { get; set; }
    [Phone]
    public required string Phone { get; set; }
    [EmailAddress]
    public string? Email { get; set; }
}
