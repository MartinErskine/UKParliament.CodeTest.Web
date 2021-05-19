using System;
using UKParliament.CodeTest.Services.Models.Person;

namespace UKParliament.CodeTest.Services.Models.Room
{
    public class RoomBookingModel
    {
        public PersonModel Person { get; set; }
        public RoomModel Room { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
