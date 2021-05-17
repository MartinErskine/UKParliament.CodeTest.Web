using AutoMapper;
using UKParliament.CodeTest.Services.Models.Person;

namespace UKParliament.CodeTest.Web.Helpers.AutoMapperProfiles
{
    public class PersonProfile : Profile
    {
        public PersonProfile()
        {
            CreateMap<PersonProfile, PersonInfo>().ReverseMap();
        }
    }
}
