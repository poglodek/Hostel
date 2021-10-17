using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Hostel_System.Core.IServices;
using Hostel_System.Database;
using Hostel_System.Dto.Dto;

namespace Hostel_System.Core.Services
{
    public class RoomServices : IRoomServices
    {
        private readonly HostelSystemDbContext _hostelSystemDbContext;
        private readonly IMapper _mapper;

        public RoomServices(HostelSystemDbContext hostelSystemDbContext
            , IMapper mapper)
        {
            _hostelSystemDbContext = hostelSystemDbContext;
            _mapper = mapper;
        }

        public IEnumerable<RoomDto> GetRooms(int page)
        {
            var rooms = _hostelSystemDbContext
                .Rooms
                .Skip(page * 10)
                .Take(10)
                .AsEnumerable();
            var roomsDto = _mapper.Map<IEnumerable<RoomDto>>(rooms);
            return roomsDto;
        }

        public RoomDto GetRoom(int id)
        {
            var room = _hostelSystemDbContext
                .Rooms
                .FirstOrDefault(x => x.Id == id);
            return _mapper.Map<RoomDto>(room);
        }
    }
}
