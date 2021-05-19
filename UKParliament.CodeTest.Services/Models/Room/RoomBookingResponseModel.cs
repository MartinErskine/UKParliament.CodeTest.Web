using System;
using System.Collections.Generic;
using UKParliament.CodeTest.Data.Domain;
using UKParliament.CodeTest.Services.Models.Person;

namespace UKParliament.CodeTest.Services.Models.Room
{
    public class RoomBookingResponseModel
    {
        public PersonModel Person { get; set; }

        public RoomModel Room { get; set; }


        //public string Name { get; set; }

        //public ICollection<RoomBookingModel> Bookings { get; set; }
    }
}
