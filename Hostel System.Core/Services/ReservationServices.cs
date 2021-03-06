using AutoMapper;
using Hostel_System.Core.Exception;
using Hostel_System.Core.IServices;
using Hostel_System.Database;
using Hostel_System.Database.Entity;
using Hostel_System.Dto.Dto;
using Microsoft.EntityFrameworkCore;

namespace Hostel_System.Core.Services
{
    public class ReservationServices : IReservationServices
    {
        private readonly HostelSystemDbContext _hostelSystemDbContext;
        private readonly IMapper _mapper;
        private readonly IUserServices _userServices;
        private readonly IUserContextServices _userContextServices;
        private readonly IRoomServices _roomServices;

        public ReservationServices(HostelSystemDbContext hostelSystemDbContext
            , IMapper mapper,
            IUserServices userServices,
            IUserContextServices userContextServices,
            IRoomServices roomServices)
        {
            _hostelSystemDbContext = hostelSystemDbContext;
            _mapper = mapper;
            _userServices = userServices;
            _userContextServices = userContextServices;
            _roomServices = roomServices;
        }

        public bool Book(RoomReservationDto roomReservationDto)
        {
            var reservation = MapToReservation(roomReservationDto);

            if (!IsRoomFree(reservation))
                return false;
            reservation.Status = "New";
            _hostelSystemDbContext.Reservations.Add(reservation);
            _hostelSystemDbContext.SaveChanges();
            return true;
        }

        public bool BookToGuest(BookToGuestDto bookToGuestDto)
        {
            var user = _userServices.GetUserByEmail(bookToGuestDto.GuestEmail);
            var reservation = _mapper.Map<Reservation>(bookToGuestDto);
            reservation.BookingRoom = _roomServices.GetRoomByName(bookToGuestDto.RoomName);
            reservation.TotalCost = ((bookToGuestDto.BookingTo - bookToGuestDto.BookingFrom).TotalDays + 1) *
                                    reservation.BookingRoom.PriceForDay;
            if (!IsRoomFree(reservation))
                return false;
            reservation.BookingUser = user;
            reservation.Status = "New";
            _hostelSystemDbContext.Reservations.Add(reservation);
            _hostelSystemDbContext.SaveChanges();
            return true;
        }

        public bool IsRoomFree(Reservation reservation)
        {
            return !_hostelSystemDbContext.Reservations
                  .Any(x => x.BookingRoom == reservation.BookingRoom && x.BookingFrom < reservation.BookingFrom &&
                            x.BookingTo > reservation.BookingTo);
        }

        public IEnumerable<RoomReservedDto> GetMyReservations()
        {
            return _mapper.Map<IEnumerable<RoomReservedDto>>(_hostelSystemDbContext
                .Reservations
                .Include(x => x.BookingUser)
                .Include(x => x.BookingRoom)
                .Where(x => x.BookingUser.Id == _userContextServices.GetUserId())
                .AsEnumerable());
        }

        public RoomReservedDto GetReservationDtoById(int id)
        {
            var reservation = _hostelSystemDbContext
                .Reservations
                .Include(x => x.BookingRoom)
                .Include(x => x.BookingUser)
                .FirstOrDefault(x => x.Id == id);
            if (reservation.BookingUser.Id != _userContextServices.GetUserId())
                throw new ForbiddenException("Forbidden");
            return _mapper.Map<RoomReservedDto>(reservation);
        }

        public RoomReservedDto GetActualReservationByRoomId(int id)
        {
            return _mapper.Map<RoomReservedDto>(_hostelSystemDbContext
                .Reservations
                .Include(x => x.BookingRoom)
                .Include(x => x.BookingUser)
                .FirstOrDefault(x => x.BookingRoom.Id == id && x.BookingFrom < DateTime.Now && x.BookingTo > DateTime.Now));
        }
        public int GetActualReservationIdByRoomId(int id)
        {
            var reservation = _hostelSystemDbContext
                .Reservations
                .Include(x => x.BookingRoom)
                .Include(x => x.BookingUser)
                .FirstOrDefault(x =>
                    x.BookingRoom.Id == id && x.BookingFrom < DateTime.Now && x.BookingTo > DateTime.Now);
            return reservation.Id == -1 ? -1 : reservation.Id;
        }

        public IEnumerable<ReservedInfoDto> GetAllReservations(int page)
        {
            return _mapper.Map<IEnumerable<ReservedInfoDto>>(_hostelSystemDbContext
                .Reservations
                .Include(x => x.BookingUser)
                .Include(x => x.BookingRoom)
                .Skip(page * 10)
                .Take(10)
                .AsEnumerable());
        }

        public IEnumerable<ReservedInfoDto> GetAllReservationsFromRoom(string searchParse)
        {
            return _mapper.Map<IEnumerable<ReservedInfoDto>>(_hostelSystemDbContext
                .Reservations
                .Include(x => x.BookingUser)
                .Include(x => x.BookingRoom)
                .Where(x => x.BookingRoom.RoomName.Contains(searchParse))
                .AsEnumerable());
        }

        public IEnumerable<ReservedInfoDto> GetAllReservationsForDate(string searchParse)
        {
            return _mapper.Map<IEnumerable<ReservedInfoDto>>(_hostelSystemDbContext
                .Reservations
                .Include(x => x.BookingUser)
                .Include(x => x.BookingRoom)
                .Where(x => x.BookingFrom < DateTime.Parse(searchParse) && x.BookingTo > DateTime.Parse(searchParse))
                .AsEnumerable());
        }

        public IEnumerable<ReservedInfoDto> GetAllReservationsForStatus(string searchParse)
        {
            return _mapper.Map<IEnumerable<ReservedInfoDto>>(_hostelSystemDbContext
                .Reservations
                .Include(x => x.BookingUser)
                .Include(x => x.BookingRoom)
                .Where(x => x.Status.ToUpper().Contains(searchParse.ToUpper()))
                .AsEnumerable());
        }

        public IEnumerable<ReservedInfoDto> GetAllReservationsForUserName(string searchParse)
        {
            return _mapper.Map<IEnumerable<ReservedInfoDto>>(_hostelSystemDbContext
                .Reservations
                .Include(x => x.BookingUser)
                .Include(x => x.BookingRoom)
                .Where(x => (x.BookingUser.FirstName + " " + x.BookingUser.LastName).ToUpper().Contains(searchParse.ToUpper()))
                .AsEnumerable());
        }

        public IEnumerable<ReservedInfoDto> GetAllReservationsForUserEmail(string searchParse)
        {
            return _mapper.Map<IEnumerable<ReservedInfoDto>>(_hostelSystemDbContext
                .Reservations
                .Include(x => x.BookingUser)
                .Include(x => x.BookingRoom)
                .Where(x => x.BookingUser.Email.ToUpper().Contains(searchParse.ToUpper()))
                .AsEnumerable());
        }

        public void UpDateStatus(int id, string status)
        {
            var reservation = GetReservationById(id);
            reservation.Status = status;
            _hostelSystemDbContext.SaveChanges();
        }

        public void RemoveReservation(int id)
        {
            _hostelSystemDbContext.Reservations.Remove(GetReservationById(id));
            _hostelSystemDbContext.SaveChanges();
        }

        private Reservation GetReservationById(int id)
        {
            return _hostelSystemDbContext
                .Reservations
                .FirstOrDefault(x => x.Id == id);
        }
        private Reservation MapToReservation(RoomReservationDto roomReservationDto)
        {
            var dateFrom = DateTime.Parse(roomReservationDto.BookingFrom.ToShortDateString()).AddHours(11);
            var dateTo = DateTime.Parse(roomReservationDto.BookingTo.ToShortDateString()).AddHours(14);
            return new Reservation
            {
                BookingTo = dateTo,
                BookingFrom = dateFrom,
                BookingRoom = _roomServices.GetRoom(roomReservationDto.Id),
                Status = "New",
                BookingUser = _userServices.GetUserById(_userContextServices.GetUserId()),
                AdditionalInformation = roomReservationDto.AdditionalInformation,
                TotalCost = ((dateTo - dateFrom).TotalDays + 1) * roomReservationDto.PriceForDay
            };
        }
    }
}
