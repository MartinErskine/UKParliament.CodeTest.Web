using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UKParliament.CodeTest.Data.Helpers;

namespace UKParliament.CodeTest.Data.Domain
{
    [Table("Rooms")]
    public class Room
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [Unique("Name")]
        public string Name { get; set; }
        
        public ICollection<RoomBooking> Bookings { get; set; }
    }
}