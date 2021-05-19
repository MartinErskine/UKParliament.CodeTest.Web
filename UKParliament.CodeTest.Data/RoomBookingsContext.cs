using Microsoft.EntityFrameworkCore;
using UKParliament.CodeTest.Data.Domain;

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
        public DbSet<RoomTime> RoomTimes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Room>()
                .HasMany(b => b.Bookings)
                .WithOne(c => c.Room)
                .IsRequired();
        }
    }
}