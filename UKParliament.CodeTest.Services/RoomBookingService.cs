using AutoMapper;
using UKParliament.CodeTest.Data;

namespace UKParliament.CodeTest.Services
{
    public class RoomBookingService
    {
        private readonly RoomBookingsContext _context;
        private readonly IMapper _mapper;

        public RoomBookingService(RoomBookingsContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


    }
}
