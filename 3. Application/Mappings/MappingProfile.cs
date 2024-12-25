using _2._Domain.Entities;
using _3._Application.DTOs;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3._Application.Mappings
{
    internal class MappingProfile:Profile 
    {
        public MappingProfile() 
        {
            CreateMap<Hotel, HotelDTO>().ReverseMap();
            CreateMap<Room, RoomDTO>().ReverseMap();
            CreateMap<Feedback, FeedbackDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<Booking, BookingDTO>().ReverseMap();
            CreateMap<Invoice, InvoiceDTO>().ReverseMap();
            CreateMap<RoomBooking, RoomBookingDTO>().ReverseMap();
            CreateMap<ServiceBooking, ServiceBookingDTO>().ReverseMap();
            CreateMap<UserBooking, UserBookingDTO>().ReverseMap();
        }
    }
}
