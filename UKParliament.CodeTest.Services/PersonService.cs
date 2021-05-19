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
using UKParliament.CodeTest.Services.Models.Person;

namespace UKParliament.CodeTest.Services
{
    public class PersonService : IPersonService
    {
        private readonly RoomBookingsContext _context;
        private readonly IMapper _mapper;

        public PersonService(RoomBookingsContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<PersonModel>> GetAsync(int personId)
        {
            try
            {
                var person = await _context.People.FirstOrDefaultAsync(f => f.Id == personId);

                if (person == null)
                {
                    return new ServiceResponse<PersonModel>
                    {
                        ErrorCode = HttpStatusCode.NotFound,
                        ErrorDescription = "Person does not exist"
                    };
                }

                return new ServiceResponse<PersonModel>
                {
                    Data = _mapper.Map<PersonModel>(person)
                };
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return new ServiceResponse<PersonModel>
                {
                    ErrorCode = HttpStatusCode.InternalServerError,
                    ErrorDescription = "Internal Server Error"
                };
            }
        }

        public async Task<ServiceResponse<List<PersonModel>>> SearchAsync(string name)
        {
            try
            {
                var person = await _context.People.Where(w => w.Name == name).ToListAsync();

                if (person == null)
                {
                    return new ServiceResponse<List<PersonModel>>
                    {
                        ErrorCode = HttpStatusCode.NotFound,
                        ErrorDescription = "Person does not exist"
                    };
                }

                return new ServiceResponse<List<PersonModel>>
                {
                    Data = _mapper.Map<List<PersonModel>>(person)
                };
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return new ServiceResponse<List<PersonModel>>
                {
                    ErrorCode = HttpStatusCode.InternalServerError,
                    ErrorDescription = "Internal Server Error"
                };
            }
        }

        public async Task<ServiceResponse<PersonModel>> PostAsync(PersonRequestModel personRequestModel)
        {
            try
            {
                var person = await _context.People.AsNoTracking().FirstOrDefaultAsync(f => f.Name == personRequestModel.Name && f.DateOfBirth == personRequestModel.DateOfBirth);

                if (person != null)
                {
                    return new ServiceResponse<PersonModel>
                    {
                        ErrorCode = HttpStatusCode.Conflict,
                        ErrorDescription = "Person already exists"
                    };
                }

                var newPerson = _mapper.Map<Person>(personRequestModel);

                _context.Entry(newPerson).State = EntityState.Added;

                if (await _context.SaveChangesAsync() > 0)
                {
                    return new ServiceResponse<PersonModel>
                    {
                        Data = _mapper.Map<PersonModel>(newPerson)
                    };
                }

                return new ServiceResponse<PersonModel>
                {
                    ErrorCode = HttpStatusCode.InternalServerError,
                    ErrorDescription = "Internal Server Error"
                };
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return new ServiceResponse<PersonModel>
                {
                    ErrorCode = HttpStatusCode.InternalServerError,
                    ErrorDescription = "Internal Server Error"
                };
            }
        }

        public async Task<ServiceResponse<PersonModel>> PutAsync(PersonPutModel personPutModel)
        {
            try
            {
                var person = await _context.People.AsNoTracking().FirstOrDefaultAsync(f => f.Id == personPutModel.Id);

                if (person == null)
                {
                    return new ServiceResponse<PersonModel>
                    {
                        ErrorCode = HttpStatusCode.NotFound,
                        ErrorDescription = "Person does not exist"
                    };
                }

                var updatedPerson = _mapper.Map<Person>(personPutModel);

                _context.Entry(updatedPerson).State = EntityState.Modified;

                if (await _context.SaveChangesAsync() > 0)
                {
                    return new ServiceResponse<PersonModel>
                    {
                        Data = _mapper.Map<PersonModel>(updatedPerson)
                    };
                }

                return new ServiceResponse<PersonModel>
                {
                    ErrorCode = HttpStatusCode.InternalServerError,
                    ErrorDescription = "Internal Server Error"
                };

            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return new ServiceResponse<PersonModel>
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
                        ErrorCode = HttpStatusCode.NotFound,
                        ErrorDescription = "Person does not exist"
                    };
                }

                //TODO: Check if Person is assigned to any current Bookings.
                //



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
