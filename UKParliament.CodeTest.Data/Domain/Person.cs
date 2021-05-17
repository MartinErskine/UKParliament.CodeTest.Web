using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UKParliament.CodeTest.Data.Domain
{
    [Table("People")]
    public class Person
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        [Required]
        public DateTime DateOfBirth { get; set; }
    }
}