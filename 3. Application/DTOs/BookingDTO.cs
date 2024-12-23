namespace _3._Application.DTOs;

public class BookingDTO
{
    public int BookingId { get; set; }

    public required DateTimeOffset BookingTime { get; set; }

    public required int RoomId { get; set; }

    public required DateTimeOffset CheckinDate { get; set; }

    public required DateTimeOffset CheckoutDate { get; set; }

    public required int Status { get; set; }

    public required int GuestId { get; set; }

    public required IList<InvoiceDTO> Invoices { get; set; } = [];

    public required IList<RoomBookingDTO> RoomBookingDTOs { get; set; } = [];

    public required IList<ServiceBookingDTO> ServiceBookingDTOs { get; set; } = [];

    public required IList<GuestBookingDTO> GuestBookingDTOs { get; set; } = [];
}
