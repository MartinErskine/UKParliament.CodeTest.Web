using System.Collections.Generic;
using System.Threading.Tasks;
using UKParliament.CodeTest.Services.Helpers;
using UKParliament.CodeTest.Services.Models.Room;

namespace UKParliament.CodeTest.Services.Interfaces
{
    public interface IRoomService
    {
        Task<ServiceResponse<RoomModel>> GetAsync(int roomId);
        Task<ServiceResponse<List<RoomModel>>> SearchAsync(string name);
        Task<ServiceResponse<RoomModel>> PostAsync(RoomRequestModel roomRequestModel);
        Task<ServiceResponse<RoomModel>> PutAsync(RoomPutModel roomPutModel);
        Task<ServiceResponse<string>> DeleteAsync(int id);
        Task<ServiceResponse<List<RoomBookingModel>>> GetBookingsAsync();
        Task<ServiceResponse<RoomBookingModel>> GetRoomBookingAsync(int id);
        Task<ServiceResponse<List<RoomBookingInfo>>> BookRoomAsync(RoomBookingRequestModel roomBookingRequestModel);
        Task<ServiceResponse<List<RoomBookingInfo>>> Availability();
    }
}