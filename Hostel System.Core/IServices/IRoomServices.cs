using Hostel_System.Database.Entity;
using Hostel_System.Dto.Dto;

namespace Hostel_System.Core.IServices;

public interface IRoomServices
{
    IEnumerable<RoomDto> GetRooms(int page);
    RoomDto GetRoomDto(int id);
    Room GetRoom(int id);
    Room GetRoomByName(string roomName);
}