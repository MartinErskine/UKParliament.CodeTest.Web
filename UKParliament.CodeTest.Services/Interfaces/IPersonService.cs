using System.Collections.Generic;
using System.Threading.Tasks;
using UKParliament.CodeTest.Services.Helpers;
using UKParliament.CodeTest.Services.Models.Person;

namespace UKParliament.CodeTest.Services.Interfaces
{
    public interface IPersonService
    {
        Task<ServiceResponse<PersonInfo>> GetAsync(int personId);
        Task<ServiceResponse<List<PersonInfo>>> SearchAsync(string name);
        Task<ServiceResponse<PersonInfo>> PostAsync(PersonRequestModel personRequestModel);
        Task<ServiceResponse<PersonInfo>> PutAsync(PersonPutModel personPutModel);
    }
}