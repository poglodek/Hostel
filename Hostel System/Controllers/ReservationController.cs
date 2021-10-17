using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hostel_System.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hostel_System.Controllers
{
    [Authorize]
    public class ReservationController : Controller
    {
        public ReservationController()
        {
            
        }
        [HttpGet]
        public IActionResult Book(int id)
        {
            return View();
        }
        [HttpPost]
        public IActionResult Book(RoomReservationModel model)
        {
            return View();
        }
    }
}
