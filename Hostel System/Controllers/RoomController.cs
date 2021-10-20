using Hostel_System.Core.IServices;
using Hostel_System.Mappers;
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
            return View(_mapper.Map<IEnumerable<Hostel_System.Model.RoomModel>>(rooms));
        }
        [Route("Details/{id}")]
        public IActionResult Details(int id)
        {
            return View(_mapper.Map<Hostel_System.Model.RoomModel>(_roomServices.GetRoomDto(id)));
        }
        [Route("RoomName")]
        public IActionResult RoomName([FromQuery] string SearchParse)
        {
            if (int.TryParse(SearchParse, out var nevermind))
                return RedirectToAction("Details", "Room", new { id = SearchParse });
            return RedirectToAction("Index", "Room", new { page = 0 });
        }

    }
}
