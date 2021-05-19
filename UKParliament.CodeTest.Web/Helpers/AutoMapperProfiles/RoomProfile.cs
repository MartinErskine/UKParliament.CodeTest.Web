using System.IO.Compression;
using AutoMapper;
using UKParliament.CodeTest.Data.Domain;
using UKParliament.CodeTest.Services.Models.Room;

namespace UKParliament.CodeTest.Web.Helpers.AutoMapperProfiles
{
    public class RoomProfile : Profile
    {
        public RoomProfile()
        {
            CreateMap<Room, RoomModel>()
                .ReverseMap();
            CreateMap<Room, RoomPutModel>()
                .ReverseMap();
            CreateMap<Room, RoomRequestModel>()
                .ReverseMap();

            CreateMap<RoomBooking, RoomBookingInfo>()
                .ReverseMap();

            CreateMap<RoomBooking, RoomBookingModel>()
                //.ForPath(dest => dest.Person, opt => opt.MapFrom(src => src.Person))
                //.ForPath(dest => dest.Room, opt => opt.MapFrom(src => src.Room))
                .ReverseMap();

            CreateMap<RoomBookingResponseModel, Room>()
                .ForPath(dest => dest.Bookings, opt => opt.MapFrom(src => src.Room))
                .ReverseMap();

            CreateMap<RoomModel, RoomBookingModel>();

            CreateMap<RoomBooking, RoomBookingResponseModel>();


        }
    }
}
