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
            _hostelSystemDbContext.Reservations.Add(reservation);
            _hostelSystemDbContext.SaveChanges();
            return true;
        }

        public bool IsRoomFree(Reservation reservation)
        {
            var free = _hostelSystemDbContext.Reservations
                 .Any(x => x.BookingRoom == reservation.BookingRoom && x.BookingFrom < reservation.BookingFrom &&
                           x.BookingTo > reservation.BookingTo);
            return !free;
        }

        public IEnumerable<RoomReservedDto> GetMyReservations()
        {
            var reservations = _hostelSystemDbContext
                .Reservations
                .Include(x => x.BookingUser)
                .Include(x => x.BookingRoom)
                .Where(x => x.BookingUser.Id == _userContextServices.GetUserId())
                .AsEnumerable();
            return _mapper.Map<IEnumerable<RoomReservedDto>>(reservations);
        }

        public RoomReservedDto GetReservationById(int id)
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
