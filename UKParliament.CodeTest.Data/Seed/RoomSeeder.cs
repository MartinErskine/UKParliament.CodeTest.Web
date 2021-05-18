﻿using System;
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
            }
        }
    }
}
