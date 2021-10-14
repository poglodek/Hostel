using Hostel_System.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Hostel_System.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

    }
}