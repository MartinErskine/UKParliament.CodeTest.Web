using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UKParliament.CodeTest.Data;
using UKParliament.CodeTest.Data.Domain;
using UKParliament.CodeTest.Services.Helpers;
using UKParliament.CodeTest.Services.Models.Room;

namespace UKParliament.CodeTest.Services
{
    public class RoomService : IRoomService
    {
        private readonly RoomBookingsContext _context;
        private readonly IMapper _mapper;

        public RoomService(RoomBookingsContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public async Task<ServiceResponse<RoomInfo>> GetAsync(int roomId)
        {
            try
            {
                var room = await _context.Rooms.FirstOrDefaultAsync(f => f.Id == roomId);

                if (room == null)
                {
                    return new ServiceResponse<RoomInfo>
                    {
                        ErrorCode = HttpStatusCode.Conflict,
                        ErrorDescription = "Room does not exist"
                    };
                }

                return new ServiceResponse<RoomInfo>
                {
                    Data = _mapper.Map<RoomInfo>(room)
                };
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return new ServiceResponse<RoomInfo>
                {
                    ErrorCode = HttpStatusCode.InternalServerError,
                    ErrorDescription = "Internal Server Error"
                };
            }
        }

        public async Task<ServiceResponse<List<RoomInfo>>> SearchAsync(string name)
        {
            try
            {
                var room = await _context.Rooms.Where(w => w.Name == name).ToListAsync();

                if (room == null)
                {
                    return new ServiceResponse<List<RoomInfo>>
                    {
                        ErrorCode = HttpStatusCode.Conflict,
                        ErrorDescription = "Room does not exist"
                    };
                }

                return new ServiceResponse<List<RoomInfo>>
                {
                    Data = _mapper.Map<List<RoomInfo>>(room)
                };
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return new ServiceResponse<List<RoomInfo>>
                {
                    ErrorCode = HttpStatusCode.InternalServerError,
                    ErrorDescription = "Internal Server Error"
                };
            }
        }

        public async Task<ServiceResponse<RoomInfo>> PostAsync(RoomRequestModel roomRequestModel)
        {
            try
            {
                var room = await _context.People.AsNoTracking().FirstOrDefaultAsync(f => f.Name == roomRequestModel.Name);

                if (room != null)
                {
                    return new ServiceResponse<RoomInfo>
                    {
                        ErrorCode = HttpStatusCode.Conflict,
                        ErrorDescription = "Room already exists"
                    };
                }

                var newRoom = _mapper.Map<Room>(roomRequestModel);

                _context.Entry(newRoom).State = EntityState.Added;

                if (await _context.SaveChangesAsync() > 0)
                {
                    return new ServiceResponse<RoomInfo>
                    {
                        Data = _mapper.Map<RoomInfo>(newRoom)
                    };
                }

                return new ServiceResponse<RoomInfo>
                {
                    ErrorCode = HttpStatusCode.InternalServerError,
                    ErrorDescription = "Internal Server Error"
                };
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return new ServiceResponse<RoomInfo>
                {
                    ErrorCode = HttpStatusCode.InternalServerError,
                    ErrorDescription = "Internal Server Error"
                };
            }
        }

        public async Task<ServiceResponse<RoomInfo>> PutAsync(RoomPutModel roomPutModel)
        {
            try
            {
                var room = await _context.People.AsNoTracking().FirstOrDefaultAsync(f => f.Id == roomPutModel.Id);

                if (room == null)
                {
                    return new ServiceResponse<RoomInfo>
                    {
                        ErrorCode = HttpStatusCode.Conflict,
                        ErrorDescription = "Room does not exist"
                    };
                }

                var updatedRoom = _mapper.Map<Person>(roomPutModel);

                _context.Entry(updatedRoom).State = EntityState.Modified;

                if (await _context.SaveChangesAsync() > 0)
                {
                    return new ServiceResponse<RoomInfo>
                    {
                        Data = _mapper.Map<RoomInfo>(updatedRoom)
                    };
                }

                return new ServiceResponse<RoomInfo>
                {
                    ErrorCode = HttpStatusCode.InternalServerError,
                    ErrorDescription = "Internal Server Error"
                };

            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return new ServiceResponse<RoomInfo>
                {
                    ErrorCode = HttpStatusCode.InternalServerError,
                    ErrorDescription = "Internal Server Error"
                };
            }
        }

        public async Task<ServiceResponse<string>> DeleteAsync(int id)
        {
            try
            {
                var roomBookings = await _context.RoomBookings.Include(i => i.Person).FirstOrDefaultAsync(f => f.PersonId == id);

                if (roomBookings != null)
                {
                    return new ServiceResponse<string>
                    {
                        ErrorCode = HttpStatusCode.Conflict,
                        ErrorDescription = "Person does not exist"
                    };
                }





                return null;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return new ServiceResponse<string>
                {
                    ErrorCode = HttpStatusCode.InternalServerError,
                    ErrorDescription = "Internal Server Error"
                };
            }
        }
    }
}
