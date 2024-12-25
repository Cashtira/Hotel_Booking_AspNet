namespace _3._Application.DTOs;

public sealed class RoomDTO
{
    public int RoomId { get; set; }

    public required string Name { get; set; }

    public required int HotelId { get; set; }

    public required int RoomTypeId { get; set; }

    public required int Status { get; set; }

    public HotelDTO? Hotel { get; set; } = null;

    public RoomTypeDTO? RoomType { get; set; } = null;

    public required IList<BookingDTO> BookingDTOs { get; set; } = [];

    public required IList<MaintenanceDTO> Maintenances { get; set; } = [];

    public required IList<RoomBookingDTO> RoomBookingDTOs { get; set; } = [];

    public required IList<HousekeepingDTO> HousekeepingDTOs { get; set; } = [];
}
