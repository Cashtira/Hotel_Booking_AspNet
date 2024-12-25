using _2._Domain.Enums;

namespace _3._Application.DTOs;

public sealed class BookingDTO
{
    public int BookingId { get; set; }

    public required DateTimeOffset BookingTime { get; set; }

    public required string UserId { get; set; }

    public required DateTimeOffset CheckinDate { get; set; }

    public required DateTimeOffset CheckoutDate { get; set; }

    public required BookingStatus Status { get; set; }

    public UserDTO? User { get; set; } = null;

    public IList<InvoiceDTO> Invoices { get; set; } = [];

    public IList<RoomBookingDTO> RoomBookingDTOs { get; set; } = [];

    public IList<ServiceBookingDTO> ServiceBookingDTOs { get; set; } = [];

    public IList<UserBookingDTO> GuestBookingDTOs { get; set; } = [];
}
