using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Hostel_System.Controllers
{
    public class ErrorController : Controller
    {
        [Route("/Forbidden")]
        public IActionResult Forbidden()
        {
            return View();
        }
    }
}
