using AutoMapper;
using Hostel_System.Core.Exception;
using Hostel_System.Core.IServices;
using Hostel_System.Database;
using Hostel_System.Database.Entity;
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

        public RoomDto GetRoomDto(int id)
        {
            var room = _hostelSystemDbContext
                .Rooms
                .FirstOrDefault(x => x.Id == id);
            if (room is null) throw new NotFoundException("Room not found.");
            return _mapper.Map<RoomDto>(room);
        }
        public Room GetRoom(int id)
        {
            return _hostelSystemDbContext
                .Rooms
                .FirstOrDefault(x => x.Id == id);
        }
    }
}
