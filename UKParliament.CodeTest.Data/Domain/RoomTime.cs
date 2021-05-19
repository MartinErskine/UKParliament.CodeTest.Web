using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UKParliament.CodeTest.Data.Domain
{
    [Table("RoomTimes")]
    public class RoomTime
    {
        [Key]
        public int Id { get; set; }

        public int RoomId { get; set; } 
        public Room Room { get; set; }

        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}
