using Hostel_System.Core.IServices;
using Hostel_System.Dto.Dto;
using Hostel_System.Mappers;
using Hostel_System.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hostel_System.Controllers
{
    [Route("Room")]
    [Authorize]
    public class RoomController : Controller
    {
        private readonly IRoomServices _roomServices;
        private readonly HostelSystemModelMapper _mapper;

        public RoomController(IRoomServices roomServices,
            HostelSystemModelMapper mapper)
        {
            _roomServices = roomServices;
            _mapper = mapper;
        }
        [Route("page/{page}")]
        public IActionResult Index(int page)
        {
            var rooms = _roomServices.GetRooms(page);
            ViewBag.Page = page;
            return View(_mapper.Map<IEnumerable<RoomModel>>(rooms));
        }
        [Route("Details/{id}")]
        public IActionResult Details(int id)
        {
            return View(_mapper.Map<RoomModel>(_roomServices.GetRoomDto(id)));
        }
        [Route("RoomName")]
        public IActionResult RoomName([FromQuery] string SearchParse)
        {
            if (int.TryParse(SearchParse, out var nevermind))
                return RedirectToAction("Details", "Room", new { id = SearchParse });
            return RedirectToAction("Index", "Room", new { page = 0 });
        }
        [HttpGet("AddRoom")]
        [Authorize(Roles = "Admin,Manager")]
        public IActionResult AddRoom()
        {
            return View();
        }
        [HttpPost("AddRoom")]
        [Authorize(Roles = "Admin,Manager")]
        public IActionResult AddRoom(RoomModel roomModel)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Error = "All fields required!";
                return View();
            }
            if (!_roomServices.AddRoom(_mapper.Map<RoomDto>(roomModel)))
            {
                ViewBag.Error = "Room name exist!";
                return View();
            }
            ViewBag.Success = "Room Created!";
            return View();
        }

        [HttpGet("EditRoom/{id}")]
        [Authorize(Roles = "Admin,Manager")]
        public IActionResult EditRoom([FromRoute] int id)
        {
            return View(_mapper.Map<RoomModel>(_roomServices.GetRoomDto(id)));
        }
        [HttpPost("EditRoom/{id}")]
        [Authorize(Roles = "Admin,Manager")]
        public IActionResult EditRoom(RoomModel roomModel, [FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Error = "All fields required!";
                return View();
            }
            if (!_roomServices.EditRoom(_mapper.Map<RoomDto>(roomModel)))
            {
                ViewBag.Error = "Room name exist!";
                return View();
            }
            ViewBag.Success = "Room Edited Successfully!";
            return View();
        }
    }
}
