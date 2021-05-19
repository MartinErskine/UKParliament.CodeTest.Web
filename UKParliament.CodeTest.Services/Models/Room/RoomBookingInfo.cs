using System.Collections.Generic;
using UKParliament.CodeTest.Data.Domain;

namespace UKParliament.CodeTest.Services.Models.Room
{
    public class RoomBookingInfo
    {
        public int PersonId { get; set; }

        public Data.Domain.Person Person { get; set; }


        public ICollection<RoomBooking> Bookings { get; set; }
    }
}
