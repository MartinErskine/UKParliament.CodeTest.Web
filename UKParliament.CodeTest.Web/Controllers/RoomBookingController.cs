using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UKParliament.CodeTest.Services.Interfaces;
using UKParliament.CodeTest.Services.Models.Room;

namespace UKParliament.CodeTest.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "RoomsApi")]
    public class RoomBookingController : ControllerBase
    {
        private readonly IRoomService _roomService;

        public RoomBookingController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpGet]
        public async Task<ActionResult<List<RoomBookingInfo>>> Get()
        {
            var response = await _roomService.GetBookingsAsync();

            if (response.IsError)
            {
                return StatusCode((int)response.ErrorCode, response.ErrorDescription);
            }

            return Ok(response.Data);
        }

        [HttpPost]
        [Route("book-room")]
        public async Task<ActionResult<RoomBookingInfo>> Get(RoomBookingRequestModel roomBookingRequestModel)
        {

            var response = await _roomService.BookRoomAsync(roomBookingRequestModel);





            return null;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RoomBookingResponseModel>> GetRoom(int id)
        {
            var response = await _roomService.GetRoomBookingAsync(id);

            if (response.IsError)
            {
                return StatusCode((int)response.ErrorCode, response.ErrorDescription);
            }

            return Ok(response.Data);
        }
    }
}
