using Hostel_System.Database.Entity;
using Hostel_System.Dto.Dto;

namespace Hostel_System.Core.IServices;

public interface IReservationServices
{
    bool Book(RoomReservationDto roomReservationDto);
    bool IsRoomFree(Reservation reservation);
    IEnumerable<RoomReservedDto> GetMyReservations();
    RoomReservedDto GetReservationById(int id);
}