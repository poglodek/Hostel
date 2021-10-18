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
    [Authorize(Roles = "Admin,Manager")]
    [Route("Manager")]
    public class ManagerController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet("test")]
        public IActionResult Test([FromQuery]string SearchParse)
        {
            var a = SearchParse;
            return Ok(a);
        }
    }
}
