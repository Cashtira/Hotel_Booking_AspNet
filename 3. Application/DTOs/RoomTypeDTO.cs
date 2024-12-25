namespace _3._Application.DTOs;

public sealed class RoomTypeDTO
{
    public int RoomTypeId { get; set; }

    public required string Name { get; set; }

    public required int Capacity { get; set; }

    public required decimal PricePerNight { get; set; }

    public required decimal PricePerHour { get; set; }

    public string Description { get; set; } = string.Empty;

    public IList<RoomDTO> RoomDTOs { get; set; } = [];
}
