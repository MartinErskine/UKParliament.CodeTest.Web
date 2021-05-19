using System.Collections.Generic;
using System.Threading.Tasks;
using UKParliament.CodeTest.Services.Helpers;
using UKParliament.CodeTest.Services.Models.Person;

namespace UKParliament.CodeTest.Services.Interfaces
{
    public interface IPersonService
    {
        Task<ServiceResponse<PersonModel>> GetAsync(int personId);
        Task<ServiceResponse<List<PersonModel>>> SearchAsync(string name);
        Task<ServiceResponse<PersonModel>> PostAsync(PersonRequestModel personRequestModel);
        Task<ServiceResponse<PersonModel>> PutAsync(PersonPutModel personPutModel);
    }
}