using AutoMapper;
using UKParliament.CodeTest.Data.Domain;
using UKParliament.CodeTest.Services.Models.Person;

namespace UKParliament.CodeTest.Web.Helpers.AutoMapperProfiles
{
    public class PersonProfile : Profile
    {
        public PersonProfile()
        {
            CreateMap<Person, PersonInfo>().ReverseMap();
            CreateMap<Person, PersonPutModel>().ReverseMap();

        }
    }
}
