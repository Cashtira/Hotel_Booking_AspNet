using MVCmodel.Models;
using System.ComponentModel.DataAnnotations;

public class Feedback
{
    public int FeedbackID { get; set; }
    public int GuestID { get; set; }
    public int HotelID { get; set; }

    [Range(1, 5)]
    public double Rating { get; set; }

    [MaxLength(500)]
    public string? Comments { get; set; }

    public required Guest Guest { get; set; }
    public required Hotel Hotel { get; set; }
}
