using Hostel_System.Core.IServices;
using Hostel_System.Dto.Dto;
using Hostel_System.Mappers;
using Hostel_System.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hostel_System.Controllers
{
    [Authorize]
    [Route("Reservation")]
    public class ReservationController : Controller
    {
        private readonly IRoomServices _roomServices;
        private readonly IReservationServices _reservationServices;
        private readonly HostelSystemModelMapper _mapper;

        public ReservationController(IRoomServices roomServices,
            IReservationServices reservationServices,
            HostelSystemModelMapper mapper)
        {
            _roomServices = roomServices;
            _reservationServices = reservationServices;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult Book(int id)
        {
            var room = _roomServices.GetRoomDto(id);
            var model = _mapper.Map<RoomReservationModel>(room);
            model.PriceForDay = room.PriceForDay;
            return View(model);
        }
        [HttpPost]
        public IActionResult Book(RoomReservationModel model)
        {
            if (!ModelState.IsValid || (model.BookingFrom - model.BookingTo).TotalDays > 0)
            {
                ViewBag.ErrorMessage = "Bad setting date!";
                return View();
            }

            var dto = _mapper.Map<RoomReservationDto>(model);
            dto.PriceForDay = model.PriceForDay;
            var result = _reservationServices.Book(dto);
            if (result == false)
            {
                ViewBag.ErrorMessage = "This Date is busy";
                return View();
            }
            return Ok("TODO, room booked!");
        }
        [Route("history")]
        public IActionResult History()
        {
            var reservations = _reservationServices.GetMyReservations();
            return View(_mapper.Map<IEnumerable<RoomReservedModel>>(reservations));
        }
        [HttpGet("DetailsReserved/{id}")]
        public IActionResult DetailsReserved(int id)
        {
            var reservation = _reservationServices.GetReservationById(id);
            return View(_mapper.Map<RoomReservedModel>(reservation));
        }
        
    }
}
