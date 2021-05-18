using System.Collections.Generic;
using UKParliament.CodeTest.Data.Domain;

namespace UKParliament.CodeTest.Services.Models.Room
{
    public class RoomRequestModel
    {
        public string Name { get; set; }

        public ICollection<RoomBooking> Bookings { get; set; }
    }
}
