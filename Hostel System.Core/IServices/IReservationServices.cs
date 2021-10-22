using Hostel_System.Database.Entity;
using Hostel_System.Dto.Dto;

namespace Hostel_System.Core.IServices;

public interface IReservationServices
{
    bool Book(RoomReservationDto roomReservationDto);
    bool BookToGuest(BookToGuestDto bookToGuestDto);
    bool IsRoomFree(Reservation reservation);
    IEnumerable<RoomReservedDto> GetMyReservations();
    RoomReservedDto GetReservationDtoById(int id);
    RoomReservedDto GetActualReservationByRoomId(int id);
    int GetActualReservationIdByRoomId(int id);
    IEnumerable<ReservedInfoDto> GetAllReservations(int page);
    IEnumerable<ReservedInfoDto> GetAllReservationsFromRoom(string searchParse);
    IEnumerable<ReservedInfoDto> GetAllReservationsForDate(string searchParse);
    IEnumerable<ReservedInfoDto> GetAllReservationsForStatus(string searchParse);
    IEnumerable<ReservedInfoDto> GetAllReservationsForUserName(string searchParse);
    IEnumerable<ReservedInfoDto> GetAllReservationsForUserEmail(string searchParse);
    void UpDateStatus(int id, string status);
    void RemoveReservation(int id);
}