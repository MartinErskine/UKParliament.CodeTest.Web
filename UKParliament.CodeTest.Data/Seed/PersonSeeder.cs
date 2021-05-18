using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UKParliament.CodeTest.Data.Domain;

namespace UKParliament.CodeTest.Data.Seed
{
    public static class PersonSeeder
    {
        public static void Initialise(IServiceProvider serviceProvider)
        {
            using (var context = new RoomBookingsContext(serviceProvider.GetRequiredService<DbContextOptions<RoomBookingsContext>>()))
            {
                if(context.People.Any())
                {
                    return;
                }
                    
                context.People.AddRange(
                    new Person { Id = 1, Name = "John Smith", DateOfBirth = new DateTime(2000, 12, 1, 12, 0, 0) },
                    new Person { Id = 2, Name = "Jane Doe", DateOfBirth = new DateTime(1965, 12, 1, 12, 0, 0) },
                    new Person { Id = 3, Name = "Jack Jones", DateOfBirth = new DateTime(1972, 12, 1, 12, 0, 0) },
                    new Person { Id = 4, Name = "Mary Murphy", DateOfBirth = new DateTime(2001, 12, 1, 12, 0, 0) },
                    new Person { Id = 5, Name = "Bob Trent", DateOfBirth = new DateTime(1983, 12, 1, 12, 0, 0) },
                    new Person { Id = 6, Name = "John Smith", DateOfBirth = new DateTime(1999, 12, 1, 12, 0, 0) }
                );

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

                if (context.RoomBookings.Any())
                {
                    return;
                }

                context.RoomBookings.AddRange(
                    new RoomBooking{Id = 1, PersonId = 6, RoomId = 2 },
                    new RoomBooking{Id = 2, PersonId = 4, RoomId = 3 }
                );

                context.SaveChanges();
            }
        }
    }
}
