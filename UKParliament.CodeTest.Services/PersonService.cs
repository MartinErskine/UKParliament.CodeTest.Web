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

        public async Task<ServiceResponse<PersonInfo>> GetAsync(int personId)
        {
            try
            {
                var person = await _context.People.FirstOrDefaultAsync(f => f.Id == personId);

                if (person == null)
                {
                    return new ServiceResponse<PersonInfo>
                    {
                        ErrorCode = HttpStatusCode.Conflict,
                        ErrorDescription = "Person does not exist"
                    };
                }

                return new ServiceResponse<PersonInfo>
                {
                    Data = _mapper.Map<PersonInfo>(person)
                };
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return new ServiceResponse<PersonInfo>
                {
                    ErrorCode = HttpStatusCode.InternalServerError,
                    ErrorDescription = "Internal Server Error"
                };
            }
        }

        public async Task<ServiceResponse<List<PersonInfo>>> SearchAsync(string name)
        {
            try
            {
                var person = await _context.People.Where(w => w.Name == name).ToListAsync();

                if (person == null)
                {
                    return new ServiceResponse<List<PersonInfo>>
                    {
                        ErrorCode = HttpStatusCode.Conflict,
                        ErrorDescription = "Person does not exist"
                    };
                }

                return new ServiceResponse<List<PersonInfo>>
                {
                    Data = _mapper.Map<List<PersonInfo>>(person)
                };
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return new ServiceResponse<List<PersonInfo>>
                {
                    ErrorCode = HttpStatusCode.InternalServerError,
                    ErrorDescription = "Internal Server Error"
                };
            }
        }

        public async Task<ServiceResponse<PersonInfo>> PostAsync(PersonRequestModel personRequestModel)
        {
            try
            {
                var person = await _context.People.FirstOrDefaultAsync(f => f.Name == personRequestModel.Name && f.DateOfBirth == personRequestModel.DateOfBirth);

                if (person != null)
                {
                    return new ServiceResponse<PersonInfo>
                    {
                        ErrorCode = HttpStatusCode.Conflict,
                        ErrorDescription = "Person already exists"
                    };
                }

                var newPerson = _mapper.Map<Person>(personRequestModel);

                _context.People.Add(newPerson);

                if (await _context.SaveChangesAsync() > 0)
                {
                    return new ServiceResponse<PersonInfo>
                    {
                        Data = _mapper.Map<PersonInfo>(newPerson)
                    };
                }

                return new ServiceResponse<PersonInfo>
                {
                    ErrorCode = HttpStatusCode.InternalServerError,
                    ErrorDescription = "Internal Server Error"
                };
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return new ServiceResponse<PersonInfo>
                {
                    ErrorCode = HttpStatusCode.InternalServerError,
                    ErrorDescription = "Internal Server Error"
                };
            }
        }

        public async Task<ServiceResponse<PersonInfo>> PutAsync(PersonRequestModel personRequestModel)
        {
            try
            {
                var person = await _context.People.FirstOrDefaultAsync(f => f.Name == personRequestModel.Name && f.DateOfBirth == personRequestModel.DateOfBirth);

                if (person == null)
                {
                    return new ServiceResponse<PersonInfo>
                    {
                        ErrorCode = HttpStatusCode.Conflict,
                        ErrorDescription = "Person does not exist"
                    };
                }

                var updatedPerson = _mapper.Map<Person>(personRequestModel);

                _context.Entry(updatedPerson).State = EntityState.Modified;

                if (await _context.SaveChangesAsync() > 0)
                {
                    return new ServiceResponse<PersonInfo>
                    {
                        Data = _mapper.Map<PersonInfo>(updatedPerson)
                    };
                }

                return new ServiceResponse<PersonInfo>
                {
                    ErrorCode = HttpStatusCode.InternalServerError,
                    ErrorDescription = "Internal Server Error"
                };

            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return new ServiceResponse<PersonInfo>
                {
                    ErrorCode = HttpStatusCode.InternalServerError,
                    ErrorDescription = "Internal Server Error"
                };
            }
        }
    }
}
