using Hostel_System.Dto.Dto;

namespace Hostel_System.Core.IServices;

public interface IRoomServices
{
    IEnumerable<RoomDto> GetRooms(int page);
    RoomDto GetRoom(int id);
}