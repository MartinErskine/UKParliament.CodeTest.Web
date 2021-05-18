using System.Collections.Generic;
using System.Threading.Tasks;
using UKParliament.CodeTest.Services.Helpers;
using UKParliament.CodeTest.Services.Models.Room;

namespace UKParliament.CodeTest.Services
{
    public interface IRoomService
    {
        Task<ServiceResponse<RoomInfo>> GetAsync(int roomId);
        Task<ServiceResponse<List<RoomInfo>>> SearchAsync(string name);
        Task<ServiceResponse<RoomInfo>> PostAsync(RoomRequestModel roomRequestModel);
        Task<ServiceResponse<RoomInfo>> PutAsync(RoomPutModel roomPutModel);
        Task<ServiceResponse<string>> DeleteAsync(int id);
    }
}