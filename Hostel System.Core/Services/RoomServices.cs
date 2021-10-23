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
            return _mapper.Map<IEnumerable<RoomDto>>(_hostelSystemDbContext
                .Rooms
                .Skip(page * 10)
                .Take(10)
                .AsEnumerable());
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

        public Room GetRoomByName(string roomName)
        {
            return _hostelSystemDbContext
                .Rooms
                .FirstOrDefault(x => x.RoomName.Contains(roomName));
        }

        public bool AddRoom(RoomDto roomDto)
        {
            if (RoomExist(roomDto.RoomName)) return false;
            var room = _mapper.Map<Room>(roomDto);
            _hostelSystemDbContext.Rooms.Add(room);
            _hostelSystemDbContext.SaveChanges();
            return true;
        }

        public bool EditRoom(RoomDto roomDto)
        {
            var room = GetRoom(roomDto.Id);
            if (room is null) return false;
            room.RoomName = roomDto.RoomName;
            room.PriceForDay = roomDto.PriceForDay;
            room.Description = roomDto.Description;
            room.MaxPeopleInRoom = roomDto.MaxPeopleInRoom;
            room.Phone = roomDto.Phone;
            _hostelSystemDbContext.SaveChanges();
            return true;
        }

        private bool RoomExist(string roomName) => _hostelSystemDbContext
            .Rooms
            .Any(x => x.RoomName.ToUpper().Equals(roomName.ToUpper()));
    }

}
