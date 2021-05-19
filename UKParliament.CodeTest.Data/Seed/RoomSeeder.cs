using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UKParliament.CodeTest.Data.Domain;

namespace UKParliament.CodeTest.Data.Seed
{
    public static class RoomSeeder
    {
        public static void Initialise(IServiceProvider serviceProvider)
        {
            using (var context = new RoomBookingsContext(serviceProvider.GetRequiredService<DbContextOptions<RoomBookingsContext>>()))
            {
                if (context.Rooms.Any())
                {
                    return;
                }

                context.Rooms.AddRange(
                    new Room { Id = 1, Name = "Room One" },
                    new Room { Id = 2, Name = "Room Three" },
                    new Room { Id = 3, Name = "Room Two" },
                    new Room { Id = 4, Name = "Room Four" }
                );

                context.SaveChanges();

                var roomTwo = context.Rooms.FirstOrDefault(f => f.Id == 2);
                var roomThree = context.Rooms.FirstOrDefault(f => f.Id == 3);

                context.RoomBookings.AddRange(
                    new RoomBooking { Id = 1, PersonId = 6, Room = roomTwo, StartTime = new DateTime(2021, 05,20, 9, 45,0), EndTime = new DateTime(2021, 05, 20, 10, 00, 0) },
                    new RoomBooking { Id = 2, PersonId = 4, Room = roomThree, StartTime = new DateTime(2021, 05, 20, 10, 15, 0), EndTime = new DateTime(2021, 05, 20, 11, 15, 0) },
                    new RoomBooking { Id = 3, PersonId = 1, Room = roomThree, StartTime = new DateTime(2021, 05, 20, 9, 00, 0), EndTime = new DateTime(2021, 05, 20, 10, 00, 0) }

                );

                context.SaveChanges();
            }
        }
    }
}
