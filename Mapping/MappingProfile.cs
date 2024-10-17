using AutoMapper;
using MVCmodel.Models;
using MVCmodel.DTOs;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Hotel, HotelDto>();
        CreateMap<HotelDto, Hotel>();
    }
}
