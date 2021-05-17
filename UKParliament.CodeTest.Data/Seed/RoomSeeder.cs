using Microsoft.EntityFrameworkCore;
using UKParliament.CodeTest.Data.Domain;

namespace UKParliament.CodeTest.Data.Seed
{
    internal static class RoomSeeder
    {
        internal static void Run(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Room>(entity =>
            {
                entity.HasData(
                    new Room{ Id = 1, Name = "Room One"},
                    new Room{ Id = 2, Name = "Room Three"},
                    new Room{ Id = 3, Name = "Room Two"},
                    new Room{ Id = 4, Name = "Room Four"}
                );
            });
        }
    }
}
