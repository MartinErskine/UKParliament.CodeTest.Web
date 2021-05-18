using Autofac;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.OpenApi.Models;
using UKParliament.CodeTest.Data;
using UKParliament.CodeTest.Services;
using UKParliament.CodeTest.Services.Interfaces;
using UKParliament.CodeTest.Web.Helpers.AutoMapperProfiles;

namespace UKParliament.CodeTest.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddDbContext<RoomBookingsContext>(op => op.UseInMemoryDatabase("RoomBookings"));

            services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc(
                    "PeopleApi",
                    new OpenApiInfo()
                    {
                        Title = "People API",
                        Version = "1",
                        Description = "People Operations",
                        Contact = new OpenApiContact
                        {
                            Email = "someone@uk.gov",
                            Name = "Admin"
                        }
                    }
                );

                config.SwaggerDoc(
                    "RoomsApi",
                    new OpenApiInfo()
                    {
                        Title = "Rooms API",
                        Version = "1",
                        Description = "Rooms Operations",
                        Contact = new OpenApiContact
                        {
                            Email = "someone@uk.gov",
                            Name = "Admin"
                        }
                    }
                );
            });

            services.AddSingleton(provider => new MapperConfiguration(mc =>
            {
                mc.AddProfile(new PersonProfile());
                mc.AddProfile(new RoomProfile());
            }).CreateMapper());

            services.AddScoped<IPersonService, PersonService>();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            var assembles = new List<Assembly>()
            {
                typeof(RoomBookingsContext).Assembly,
                typeof(IPersonService).Assembly
            };

            foreach (var assembly in assembles)
            {
                builder.RegisterAssemblyTypes(assembly)
                    .AsImplementedInterfaces();
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                //c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.SwaggerEndpoint("/swagger/PeopleApi/swagger.json", "People API");
                c.SwaggerEndpoint("/swagger/RoomsApi/swagger.json", "Rooms API");
                c.RoutePrefix = string.Empty;
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
