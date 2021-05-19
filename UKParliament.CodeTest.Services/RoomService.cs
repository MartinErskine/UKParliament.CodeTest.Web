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
using UKParliament.CodeTest.Services.Interfaces;
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


        public async Task<ServiceResponse<RoomModel>> GetAsync(int roomId)
        {
            try
            {
                var room = await _context.Rooms.FirstOrDefaultAsync(f => f.Id == roomId);

                if (room == null)
                {
                    return new ServiceResponse<RoomModel>
                    {
                        ErrorCode = HttpStatusCode.Conflict,
                        ErrorDescription = "Room does not exist"
                    };
                }

                return new ServiceResponse<RoomModel>
                {
                    Data = _mapper.Map<RoomModel>(room)
                };
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return new ServiceResponse<RoomModel>
                {
                    ErrorCode = HttpStatusCode.InternalServerError,
                    ErrorDescription = "Internal Server Error"
                };
            }
        }

        public async Task<ServiceResponse<List<RoomModel>>> SearchAsync(string name)
        {
            try
            {
                var room = await _context.Rooms.Where(w => w.Name == name).ToListAsync();

                if (room == null)
                {
                    return new ServiceResponse<List<RoomModel>>
                    {
                        ErrorCode = HttpStatusCode.Conflict,
                        ErrorDescription = "Room does not exist"
                    };
                }

                return new ServiceResponse<List<RoomModel>>
                {
                    Data = _mapper.Map<List<RoomModel>>(room)
                };
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return new ServiceResponse<List<RoomModel>>
                {
                    ErrorCode = HttpStatusCode.InternalServerError,
                    ErrorDescription = "Internal Server Error"
                };
            }
        }

        public async Task<ServiceResponse<RoomModel>> PostAsync(RoomRequestModel roomRequestModel)
        {
            try
            {
                var room = await _context.People.AsNoTracking().FirstOrDefaultAsync(f => f.Name == roomRequestModel.Name);

                if (room != null)
                {
                    return new ServiceResponse<RoomModel>
                    {
                        ErrorCode = HttpStatusCode.Conflict,
                        ErrorDescription = "Room already exists"
                    };
                }

                var newRoom = _mapper.Map<Room>(roomRequestModel);

                _context.Entry(newRoom).State = EntityState.Added;

                if (await _context.SaveChangesAsync() > 0)
                {
                    return new ServiceResponse<RoomModel>
                    {
                        Data = _mapper.Map<RoomModel>(newRoom)
                    };
                }

                return new ServiceResponse<RoomModel>
                {
                    ErrorCode = HttpStatusCode.InternalServerError,
                    ErrorDescription = "Internal Server Error"
                };
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return new ServiceResponse<RoomModel>
                {
                    ErrorCode = HttpStatusCode.InternalServerError,
                    ErrorDescription = "Internal Server Error"
                };
            }
        }

        public async Task<ServiceResponse<RoomModel>> PutAsync(RoomPutModel roomPutModel)
        {
            try
            {
                var room = await _context.People.AsNoTracking().FirstOrDefaultAsync(f => f.Id == roomPutModel.Id);

                if (room == null)
                {
                    return new ServiceResponse<RoomModel>
                    {
                        ErrorCode = HttpStatusCode.Conflict,
                        ErrorDescription = "Room does not exist"
                    };
                }

                var updatedRoom = _mapper.Map<Person>(roomPutModel);

                _context.Entry(updatedRoom).State = EntityState.Modified;

                if (await _context.SaveChangesAsync() > 0)
                {
                    return new ServiceResponse<RoomModel>
                    {
                        Data = _mapper.Map<RoomModel>(updatedRoom)
                    };
                }

                return new ServiceResponse<RoomModel>
                {
                    ErrorCode = HttpStatusCode.InternalServerError,
                    ErrorDescription = "Internal Server Error"
                };

            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return new ServiceResponse<RoomModel>
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

        public async Task<ServiceResponse<List<RoomBookingModel>>> GetBookingsAsync()
        {
            try
            {
                var bookings = await _context.RoomBookings.Include(i => i.Person).Include(i => i.Room).ToListAsync();

                if (!bookings.Any())
                {
                    return new ServiceResponse<List<RoomBookingModel>>
                    {
                        ErrorCode = HttpStatusCode.NotFound,
                        ErrorDescription = "No Bookings found!"
                    };
                }

                return new ServiceResponse<List<RoomBookingModel>>
                {
                    Data = _mapper.Map<List<RoomBookingModel>>(bookings)
                };
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return new ServiceResponse<List<RoomBookingModel>>
                {
                    ErrorCode = HttpStatusCode.InternalServerError,
                    ErrorDescription = "Internal Server Error"
                };
            }
        }
            
        public async Task<ServiceResponse<RoomBookingModel>> GetRoomBookingAsync(int id)
        {
            try
            {
                var roomBooking = await _context.RoomBookings.Include(i => i.Person).Include(i => i.Room).FirstOrDefaultAsync(f => f.Id == id);

                if (roomBooking == null)
                {
                    return new ServiceResponse<RoomBookingModel>
                    {
                        ErrorCode = HttpStatusCode.NotFound,
                        ErrorDescription = "Booking not found!"
                    };
                }

                return new ServiceResponse<RoomBookingModel>
                {
                    Data = _mapper.Map<RoomBookingModel>(roomBooking)
                };
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return new ServiceResponse<RoomBookingModel>
                {
                    ErrorCode = HttpStatusCode.InternalServerError,
                    ErrorDescription = "Internal Server Error"
                };
            }
        }

        public async Task<ServiceResponse<List<RoomBookingInfo>>> BookRoomAsync(RoomBookingRequestModel roomBookingRequestModel) 
        {
            try
            {
                var roomBookings = await _context.RoomBookings.Include(i => i.Room).Where(w => w.RoomId == roomBookingRequestModel.RoomId).ToListAsync();





                return null;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return new ServiceResponse<List<RoomBookingInfo>>
                {
                    ErrorCode = HttpStatusCode.InternalServerError,
                    ErrorDescription = "Internal Server Error"
                };
            }
        }

        public async Task<ServiceResponse<List<RoomBookingInfo>>> Availability()
        {
            try
            {
                //var availableRooms = await _context.Rooms




                return null;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return new ServiceResponse<List<RoomBookingInfo>>
                {
                    ErrorCode = HttpStatusCode.InternalServerError,
                    ErrorDescription = "Internal Server Error"
                };
            }
        }
    }
}
