using Microsoft.EntityFrameworkCore;
using UKParliament.CodeTest.Data.Domain;
using UKParliament.CodeTest.Data.Seed;

namespace UKParliament.CodeTest.Data
{
    public class RoomBookingsContext : DbContext
    {
        public RoomBookingsContext(DbContextOptions<RoomBookingsContext> options)
            : base(options)
        {
        }

        public DbSet<Person> People { get; set; }

        public DbSet<Room> Rooms { get; set; }

        public DbSet<RoomBooking> RoomBookings { get; set; }


        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    this.Database.EnsureCreatedAsync();

        //    //PersonSeeder.Run(modelBuilder);
        //    RoomSeeder.Run(modelBuilder);
        //}
    }
}