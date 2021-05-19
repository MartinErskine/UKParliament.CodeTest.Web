using AutoMapper;
using UKParliament.CodeTest.Data.Domain;
using UKParliament.CodeTest.Services.Models.Person;

namespace UKParliament.CodeTest.Web.Helpers.AutoMapperProfiles
{
    public class PersonProfile : Profile
    {
        public PersonProfile()
        {
            CreateMap<Person, PersonModel>().ReverseMap();
            CreateMap<Person, PersonPutModel>().ReverseMap();
            CreateMap<Person, PersonRequestModel>().ReverseMap();

        }
    }
}
