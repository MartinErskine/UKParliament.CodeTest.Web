using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UKParliament.CodeTest.Data.Domain;

namespace UKParliament.CodeTest.Data.Seed
{
    public static class RoomBookingSeeder
    {
        public static void Initialise(IServiceProvider serviceProvider)
        {
            using (var context = new RoomBookingsContext(serviceProvider.GetRequiredService<DbContextOptions<RoomBookingsContext>>()))
            {
                if (context.RoomBookings.Any())
                {
                    return;
                }

                context.RoomBookings.AddRange(
                    new RoomBooking { Id = 1, PersonId = 6, RoomId = 2 },
                    new RoomBooking { Id = 2, PersonId = 4, RoomId = 3 }
                );

                context.SaveChanges();
            }
        }
    }
}
