using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UKParliament.CodeTest.Data.Domain
{
    [Table("RoomBookings")]
    public class RoomBooking
    {
        [Key]
        public int Id { get; set; }
        
        public int PersonId { get; set; }
        public Person Person { get; set; }


        public int RoomId { get; set; }
        public Room Room { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; } 
    }
}