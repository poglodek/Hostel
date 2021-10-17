using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Hostel_System.Core.IServices;
using Hostel_System.Database;

namespace Hostel_System.Core.Services
{
    public class ReservationServices
    {
        private readonly HostelSystemDbContext _hostelSystemDbContext;
        private readonly IMapper _mapper;
        private readonly IRoomServices _roomServices;

        public ReservationServices(HostelSystemDbContext hostelSystemDbContext
            , IMapper mapper,
            IRoomServices roomServices)
        {
            _hostelSystemDbContext = hostelSystemDbContext;
            _mapper = mapper;
            _roomServices = roomServices;
        }

    }
}
