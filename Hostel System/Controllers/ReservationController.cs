using Hostel_System.Core.IServices;
using Hostel_System.Dto.Dto;
using Hostel_System.Mappers;
using Hostel_System.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
            ViewBag.ErrorMessage = "TODO, room booked!";
            return View();
        }
        [HttpGet("BookToGuest")]
        [Authorize(Roles = "Admin,Manager")]
        public IActionResult BookToGuest()
        {
            
            return View();
        }
        [HttpPost("BookToGuest")]
        [Authorize(Roles = "Admin,Manager")]
        public IActionResult BookToGuest(BookToGuestModel model)
        {
            if (!ModelState.IsValid || (model.BookingFrom - model.BookingTo).TotalDays > 0)
            {
                ViewBag.ErrorMessage = "Bad setting date!";
                return View();
            }

            var dto = _mapper.Map<BookToGuestDto>(model);
            dto.PriceForDay = model.PriceForDay;
            var result = _reservationServices.BookToGuest(dto);
            if (result == false)
            {
                ViewBag.ErrorMessage = "This Date is busy";
                return View();
            }
            ViewBag.ErrorMessage = "TODO, room booked!";
            return View();
        }
        [Route("history")]
        public IActionResult History()
        {
            return View(_mapper.Map<IEnumerable<RoomReservedModel>>(_reservationServices.GetMyReservations()));
        }
        [HttpGet("DetailsReserved/{id}")]
        public IActionResult DetailsReserved(int id)
        {
            return View(_mapper.Map<RoomReservedModel>(_reservationServices.GetReservationDtoById(id)));
        }

        [HttpGet("ActualReservedByRoom")]
        [Authorize(Roles = "Admin,Manager")]
        public IActionResult ActualReservedByRoom(int id)
        {
            var reservationId = _reservationServices.GetActualReservationIdByRoomId(id);
            if (reservationId == -1)
            {
                return RedirectToAction("Details", "Room", new { id });
            }
            return RedirectToAction("DetailsReserved", "Reservation", new { id = reservationId });
        }
        [HttpGet("ReservationList")]
        [Authorize(Roles = "Admin,Manager")]
        public IActionResult ReservationList()
        {
            var model = JsonConvert.DeserializeObject<IEnumerable<ReservedInfoModel>>(TempData["Reservation"] as string);
            if (model is null) return RedirectToAction("Index", "Manager");
            return View(model);
        }

        [HttpGet("AllReservations")]
        [Authorize(Roles = "Admin,Manager")]
        public IActionResult AllReservations([FromQuery] string SearchParse, int page)
        {
            ViewBag.Page = page;
            var reservation = _reservationServices.GetAllReservations(page);
            TempData["Reservation"] = JsonConvert.SerializeObject(reservation);
            return View(_mapper.Map<IEnumerable<ReservedInfoModel>>(reservation));
        }
        [HttpGet("ReservationsForRoom")]
        [Authorize(Roles = "Admin,Manager")]
        public IActionResult ReservationsForRoom([FromQuery] string SearchParse)
        {
            TempData["Reservation"] = JsonConvert.SerializeObject(_reservationServices.GetAllReservationsFromRoom(SearchParse));
            return RedirectToAction("ReservationList", "Reservation");
        }

        [HttpGet("ReservationsForDate")]
        [Authorize(Roles = "Admin,Manager")]
        public IActionResult ReservationsForDate([FromQuery] string SearchParse)
        {
            if (!DateTime.TryParse(SearchParse, out var nevermind))
            {
                return RedirectToAction("Index", "Manager");
            }
            TempData["Reservation"] = JsonConvert.SerializeObject(_reservationServices.GetAllReservationsForDate(SearchParse));
            return RedirectToAction("ReservationList", "Reservation");
        }
        [HttpGet("ReservationsForStatus")]
        [Authorize(Roles = "Admin,Manager")]
        public IActionResult ReservationsForStatus([FromQuery] string SearchParse)
        {
            TempData["Reservation"] = JsonConvert.SerializeObject(_reservationServices.GetAllReservationsForStatus(SearchParse));
            return RedirectToAction("ReservationList", "Reservation");
        }
        [HttpGet("ReservationsForStatus")]
        [Authorize(Roles = "Admin,Manager")]
        public IActionResult ReservationsForUserName([FromQuery] string SearchParse)
        {
            TempData["Reservation"] = JsonConvert.SerializeObject(_reservationServices.GetAllReservationsForUserName(SearchParse));
            return RedirectToAction("ReservationList", "Reservation");
        }
        [HttpGet("ReservationsForStatus")]
        [Authorize(Roles = "Admin,Manager")]
        public IActionResult ReservationsForUserEmail([FromQuery] string SearchParse)
        {
            TempData["Reservation"] = JsonConvert.SerializeObject(_reservationServices.GetAllReservationsForUserEmail(SearchParse));
            return RedirectToAction("ReservationList", "Reservation");
        }
        [HttpPost("ChangeStatus")]
        [Authorize(Roles = "Admin,Manager")]
        public IActionResult ChangeStatus(RoomReservedModel roomReservedModel)
        {
            _reservationServices.UpDateStatus(roomReservedModel.Id, roomReservedModel.Status);
            return RedirectToAction("DetailsReserved", "Reservation", new { id = roomReservedModel.Id });
        }
        [Authorize(Roles = "Admin,Manager")]
        [Route("RemoveReservation/{id}")]
        public IActionResult RemoveReservation([FromRoute] int id)
        {
            _reservationServices.RemoveReservation(id);
            return RedirectToAction("AllReservations", "Reservation");
        }
    }
}
