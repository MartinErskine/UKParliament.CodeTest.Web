using System;

namespace UKParliament.CodeTest.Services.Models.Room
{
    public class RoomBookingRequestModel
    {
        public int PersonId { get; set; }

        public int RoomId { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
