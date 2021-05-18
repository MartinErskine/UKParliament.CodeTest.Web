using AutoMapper;
using UKParliament.CodeTest.Data.Domain;
using UKParliament.CodeTest.Services.Models.Room;

namespace UKParliament.CodeTest.Web.Helpers.AutoMapperProfiles
{
    public class RoomProfile : Profile
    {
        public RoomProfile()
        {
            CreateMap<Room, RoomInfo>().ReverseMap();
        }
    }
}
