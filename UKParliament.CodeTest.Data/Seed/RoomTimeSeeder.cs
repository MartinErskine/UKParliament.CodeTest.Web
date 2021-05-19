using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UKParliament.CodeTest.Data.Domain;

namespace UKParliament.CodeTest.Data.Seed
{
    public static class RoomTimeSeeder
    {
        public static void Initialise(IServiceProvider serviceProvider)
        {
            using (var context = new RoomBookingsContext(serviceProvider.GetRequiredService<DbContextOptions<RoomBookingsContext>>()))
            {
                if (context.RoomTimes.Any())
                {
                    return;
                }

                context.RoomTimes.AddRange(
                    new RoomTime{ Id = 1, RoomId= 1, StartTime = new TimeSpan(8), EndTime = new TimeSpan(18) },
                    new RoomTime{ Id = 1, RoomId= 2, StartTime = new TimeSpan(0, 0, 7, 5), EndTime = new TimeSpan(18) },
                    new RoomTime{ Id = 1, RoomId= 3, StartTime = new TimeSpan(8), EndTime = new TimeSpan(16) },
                    new RoomTime{ Id = 1, RoomId= 4, StartTime = new TimeSpan(8), EndTime = new TimeSpan(18) }
                );

                context.SaveChanges();
            }
        }
    }
}
